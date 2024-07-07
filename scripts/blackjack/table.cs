using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class table : Node2D
{
    private List<KeyValuePair<string, int>> deck { get; set; } = new();

    private List<int> playerCards { get; set; }
    private List<int> dealerCards { get; set; }
    private Random random = new();
    private static Vector2 PLAYER_CARD_LOCATION = new Vector2(480, 380);
    private static Vector2 DEALER_CARD_LOCATION = new Vector2(480, 100);

    private AudioStreamPlayer asp;

    private Button btnReset;
    private Button btnHit;
    private Button btnStay;
    private Button btnBack;
    private Label lblGameState;
    private Label lblPlayerSum;
    private Label lblDealerSum;
    private int numWins;
    private string cardPath;

    private void RefillDeck()
    {
        playerCards = new();
        dealerCards = new();

        var types = new[] { "h", "c", "s", "d" };
        for (int i = 1; i <= 13; i++)
            foreach (var type in types)
                deck.Add(new KeyValuePair<string, int>($"res://assets/cards/{i}{type}.png", i > 10 ? 10 : i));
    }

    private void DealDealer()
    {
        int val = Deal("d");
        if (val == 1 && dealerCards.Sum() <= 10)
            val = 11;

        dealerCards.Add(val);
    }

    private void DealPlayer()
    {
        int val = Deal("p");
        if (val == 1 && playerCards.Sum() <= 10)
            val = 11;

        playerCards.Add(val);
    }

    private int Deal(string flag)
    {
        int index = random.Next(deck.Count);
        var value = deck[index].Value;
        var path = deck[index].Key;
        deck.RemoveAt(index);

        Card card = ResourceLoader.Load<PackedScene>("res://scenes/blackjack/card.tscn").Instantiate<Card>();
        var sprite = card.GetNode<Sprite2D>("Sprite2D");
        if (flag == "d" && dealerCards.Count == 1)
        {
            cardPath = path;
            sprite.Texture = GD.Load<Texture2D>("res://assets/cards/blank_back.png") as Texture2D;
            card.Name = "Reversed";
        }
        else
            sprite.Texture = GD.Load<Texture2D>(path);

        Pos(card, flag);
        AddChild(card);
        return value;
    }

    private void Pos(Card card, string flag)
    {
        if (flag == "p")
        {
            card.GlobalPosition = PLAYER_CARD_LOCATION + new Vector2(playerCards.Count * 20, playerCards.Count * 20);
        }
        else
        {
            card.GlobalPosition = DEALER_CARD_LOCATION + new Vector2(dealerCards.Count * 20, dealerCards.Count * 20);
        }
    }

    private void HitButtonPressed()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/cardPlace.ogg");
        asp.Stream = sound;
        asp.Play();

        DealPlayer();
        updateScore();

        if (playerCards.Sum() > 21)
            GameOver();
        if (playerCards.Sum() == 21)
            BlackJack();
    }

    private void StayButtonPressed()
    {
        lblDealerSum.Visible = true;
        Card card = FindChild("Reversed", false, false) as Card;
        Sprite2D sprite = card.GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = GD.Load<Texture2D>(cardPath);
        //reverse dealers second card

        btnHit.Disabled = true;

        while (dealerCards.Sum() < 17)
        {
            DealDealer();
            updateScore();
        }

        if (dealerCards.Sum() > 21 || dealerCards.Sum() < playerCards.Sum())
            GameWon();
        else if (dealerCards.Sum() > playerCards.Sum())
            GameOver();
        else
            GameTie();
    }

    private void ResetButtonPressed()
    {
        StartNewGame();
        // GetTree().ReloadCurrentScene();
    }

    private void updateScore()
    {
        if (dealerCards.Contains(11) && dealerCards.Sum() > 21)
            dealerCards[dealerCards.IndexOf(11)] = 1;

        if (playerCards.Contains(11) && playerCards.Sum() > 21)
            playerCards[playerCards.IndexOf(11)] = 1;


        lblDealerSum.Text = dealerCards.Sum().ToString();
        lblPlayerSum.Text = playerCards.Sum().ToString();
    }

    private void BlackJack()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/victoryJingle.ogg");
        asp.Stream = sound;
        asp.Play();

        lblGameState.Text = "BLACKJACK :) :)";
        numWins++;
        GameEnd();
    }

    private void GameWon()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/victoryJingle.ogg");
        asp.Stream = sound;
        asp.Play();

        lblGameState.Text = "You Won :)";
        numWins++;
        GameEnd();
    }

    private void GameTie()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/tieJingle.ogg");
        asp.Stream = sound;
        asp.Play();

        GameEnd();
        lblGameState.Text = "It's a tie :|";
    }


    private void GameOver()
    {
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/losingJingle.ogg");
        asp.Stream = sound;
        asp.Play();

        GameEnd();
        lblGameState.Text = "You Lost :(";
    }

    private void GameEnd()
    {
        btnReset.Visible = true;
        btnHit.Disabled = true;
        btnStay.Disabled = true;
        if (numWins >= 2)
            btnBack.Visible = true;
    }

    private void BackButtonPressed()
    {
        var global = GetNode<global_var>("/root/GlobalVar");
        global.purpleFish = false;
        if (global.blueFish)
            GetTree().ChangeSceneToFile("res://scenes/Main/game.tscn");
        GetTree().ChangeSceneToFile("res://scenes/Main/end_scene.tscn");
    }

    private void StartNewGame()
    {
        foreach (var node in GetChildren().Where(child => child is Card))
            node.Free();
        // node.Dispose();


        lblDealerSum.Visible = false;
        lblGameState.Text = "";
        btnReset.Visible = false;
        btnHit.Disabled = false;
        btnStay.Disabled = false;

        RefillDeck();
        var sound = ResourceLoader.Load<AudioStreamOggVorbis>("res://assets/audio/cards/cardPlace.ogg");
        asp.Stream = sound;
        asp.Play();


        DealPlayer();
        DealDealer();

        DealPlayer();
        DealDealer();

        updateScore();


        if (playerCards.Sum() == 21)
            BlackJack();
    }

    public override void _Ready()
    {
        btnReset = GetNode<Button>("ResetButton");
        btnHit = GetNode<Button>("HitButton");
        btnStay = GetNode<Button>("StayButton");
        btnBack = GetNode<Button>("GoBackButton");
        btnBack.Visible = false;
        lblGameState = GetNode<Label>("GameStateLabel");
        lblPlayerSum = GetNode<Label>("PlayerSum");
        lblDealerSum = GetNode<Label>("DealerSum");
        playerCards = new();
        dealerCards = new();
        numWins = 0;
        asp = GetNode<AudioStreamPlayer>("SoundPlayer");

        StartNewGame();
    }
}