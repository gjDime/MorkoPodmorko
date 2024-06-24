using Godot;
using System;
using System.Collections.Generic;

public partial class table : Node2D
{
    private List<KeyValuePair<string, int>> deck { get; set; } = new();


    private int dealerSum { get; set; }
    private int playerSum { get; set; }
    private int playerCards { get; set; }
    private int dealerCards { get; set; }
    private Random random = new();
    private static Vector2 PLAYER_CARD_LOCATION = new Vector2(480, 380);
    private static Vector2 DEALER_CARD_LOCATION = new Vector2(480, 100);

    private Button btnReset;
    private Button btnHit;
    private Button btnStay;
    private Label lblGameState;
    private Label lblPlayerSum;
    private Label lblDealerSum;

    private string cardPath;

    private void RefillDeck()
    {
        playerSum = 0;
        playerCards = 0;
        dealerSum = 0;
        dealerCards = 0;
        var types = new[] { "h", "c", "s", "d" };
        for (int i = 1; i <= 13; i++)
            foreach (var type in types)
                deck.Add(new KeyValuePair<string, int>($"res://assets/cards/{i}{type}.png", i > 10 ? 10 : i));
    }

    private void DealDealer()
    {
        int val = Deal("d");
        if (val == 1 && dealerSum <= 10)
            val = 11;
        dealerSum += val;
        dealerCards++;
    }

    private void DealPlayer()
    {
        int val = Deal("p");
        if (val == 1 && playerSum <= 10)
            val = 11;
        playerSum += val;
        playerCards++;
    }

    private int Deal(string flag)
    {
        int index = random.Next(deck.Count);
        var value = deck[index].Value;
        var path = deck[index].Key;
        deck.RemoveAt(index);

        Card card = ResourceLoader.Load<PackedScene>("res://scenes/blackjack/card.tscn").Instantiate<Card>();
        Image img = new Image();
        if (flag == "d" && dealerCards == 1)
        {
            cardPath = path;
            lblDealerSum.Visible = false;
            img.Load("res://assets/cards/blank_back.png");
            card.Name = "Reversed";
        }
        else
            img.Load(path);


        var sprite = card.GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = ImageTexture.CreateFromImage(img);
        Pos(card, flag);
        AddChild(card);

        return value;
    }

    private void Pos(Card card, string flag)
    {
        if (flag == "p")
        {
            card.GlobalPosition = PLAYER_CARD_LOCATION + new Vector2(playerCards * 20, playerCards * 20);
        }
        else
        {
            card.GlobalPosition = DEALER_CARD_LOCATION + new Vector2(dealerCards * 20, dealerCards * 20);
        }
    }

    private void HitButtonPressed()
    {
        DealPlayer();

        if (playerSum > 21)
            GameOver();
        if (playerSum == 21)
            BlackJack();
        
        updateScore();
    }

    private void StayButtonPressed()
    {
        lblDealerSum.Visible = true;
        Image img = new Image();
        Card card = FindChild("Reversed", false, false) as Card;
        Sprite2D sprite = card.GetNode<Sprite2D>("Sprite2D");
        img.Load(cardPath);
        sprite.Texture = ImageTexture.CreateFromImage(img);
        //reverse dealers second card
        
        
        btnHit.Disabled = true;

        while (dealerSum < 17)
            DealDealer();

        if (dealerSum > 21)
            GameWon();
        else if (dealerSum > playerSum)
            GameOver();
        else if (dealerSum < playerSum)
            GameWon();
        else
            GameTie();


        updateScore();
    }

    private void ResetButtonPressed()
    {
        GetTree().ReloadCurrentScene();
    }

    private void updateScore()
    {
        lblDealerSum.Text = dealerSum.ToString();
        lblPlayerSum.Text = playerSum.ToString();
    }

    private void BlackJack()
    {
        GameEnd();
        lblGameState.Text = "BLACKJACK :) :)";
    }

    private void GameTie()
    {
        GameEnd();
        lblGameState.Text = "It's a tie :|";
    }

    private void GameWon()
    {
        GameEnd();
        lblGameState.Text = "You Won :)";
    }


    private void GameOver()
    {
        GameEnd();
        lblGameState.Text = "You Lost :(";
    }

    private void GameEnd()
    {
        btnReset.Visible = true;
        btnHit.Disabled = true;
        btnStay.Disabled = true;
    }

// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        btnReset = GetNode<Button>("ResetButton");
        btnHit = GetNode<Button>("HitButton");
        btnStay = GetNode<Button>("StayButton");
        lblGameState = GetNode<Label>("GameStateLabel");
        lblPlayerSum = GetNode<Label>("PlayerSum");
        lblDealerSum = GetNode<Label>("DealerSum");

        RefillDeck();

        DealPlayer();
        DealDealer();

        DealPlayer();
        DealDealer();

        updateScore();

        if (playerSum == 21)
            BlackJack();
    }

// Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}