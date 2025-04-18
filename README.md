# Игра во Godot Game Engine
## Опис на играта
Играта започнува во еден почетен „hub” свет(слика 1.)во кој ја контролираме подморницата Морко. Морко може да зборува со рибите и со тоа се почнува minigame. Целта на играта е да се победат сите риби во нивниот соодветен minigame (Pong или Blackјаck).
## Опис на minigame: Blackjack (слика 2.)
Целта е да се добие збир на карти што е поблиску до 21 отколку збирот на картите на дилерот, без да се надмине 21.
Вредности на карти : Карти од 2 до 10 имаат номинална вредност, џандар, дама и крал имаат вредност од 10, единица може да вреди или 1 или 11.
Доколку збирот на картите е помал или еднаков на 10, тогаш се смета како 11, ако пак збирот е поголем од 10 тогаш се смета како 1. 
Играта започнува со тоа што играчот и дилерот добиваат по две карти. Играчот ги гледа своите карти, а дилерот има една завртена карта.Секоја карта има еднаква веројатност на извлекување со секое започнување на играта.Играчот може да избере:
•	„Удри“ (Hit): Да побара уште една карта.
•	„Стој“ (Stand): Да остане со тековните карти. 
Со одбирање на опцијата „Стој“ започнува играњето на дилерот. Toj игра се додека неговиот збир на карти е помал од 17.
На крајот:
•	Ако играчот има збир поблиску до 21 отколку дилерот, играчот победува.
•	Ако играчот надмине 21, тогаш тој губи;Ако дилерот надмине 21, играчот победува;
•	Ако збирот им е ист, тогаш е нерешено и никој не победува.
•	Специјален случај на Blackjack победа е кога играчот ке добие единица и друга карта со вредност 10.
Скриптата за Blackjack има многу функции, за секое од копчињата, поечтокот на играта, секоја потенцијална крајна ситуација, доделување на карти на дилерот и играчот.
Како функционира се ова:
На почетокот на играта се повикуваат функциијата RefillDeck() и функциите DealPlayer() и DealDealer() 2 пати, и на крај UpdateScore().
RefillDeck() го пополнува шпилот со сите 52 карти. Шпилот е мапа со key absolute path кон текстурата за картата и value вредност на картата.
```
private void RefillDeck()
{
    playerCards = new();
    dealerCards = new();

    var types = new[] { "h", "c", "s", "d" };
    for (int i = 1; i <= 13; i++)
        foreach (var type in types)
            deck.Add(new KeyValuePair<string, int>($"res://assets/cards/{i}{type}.png", i > 10 ? 10 : i));
}
```
DealPlayer() и DealDealer() ја повикуваат функцијата Deal() со знаме со цел да се знае од кого е повикана, и проверува дали картата „А“ треба да се смета како 1 или 11.
```
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
```
Deal() -  Избира random број со горна граница deck.Count. Таа карта се додава во шпилот на играчот / дилерот. Како аргумент оваа фунцкија враќа Integer кој ја представува вредноста на извлечената карта.
```
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
```
UpdateScore() – прави пресметки за збирот на картите на играчот / дилерот и ги запишува на лабела. 
```
private void updateScore()
 {
     if (dealerCards.Contains(11) && dealerCards.Sum() > 21)
         dealerCards[dealerCards.IndexOf(11)] = 1; 

     if (playerCards.Contains(11) && playerCards.Sum() > 21)
         playerCards[playerCards.IndexOf(11)] = 1;


     lblDealerSum.Text = dealerCards.Sum().ToString();
     lblPlayerSum.Text = playerCards.Sum().ToString();
 }
```
## Опис на minigame: Pong (слика 3.)
Pong е класична видео игра со многу едноставни механики. Играчот движи лопатка за пинг понг, и целта е топчето да го одбие во голот на спротивниот играч (во овој случај,на компјутерот). 
Играта се состои од 5 поважни скрипти,  2 за движене на лопатките, 1 за топката, и 2 за секој од головите.
Топката го почнува своето движење од средината на таблата, со секој допир на некоја површина таа ја повикува .Bounce() функцијата и се одбива во одредена насока.
Тука е експериментирано топкта да има некој bounce factor, и со секое одбивање да добие додатно забрзување, но ова многу бргу станува хаотично и не е добра идеја.
Лопатката на компјутерот детектира една работа, позицијата на топчето. Ако топчето се наоѓа во левата половина (половината на играчот) компјутерот ќе чека на средината на својот гол, за да биде еднакво одаллечен до краевите и од горната и од долната страна.
Кога топчето е на половината на која се наоѓа компјутерот тој рандом бира бројќе од 0 до 6.
Ако бројот што го избере е 1 тогаш ќе се калкулира точно насоката во која треба да се движи лопатката, во спротивно таа ќе продолжи да се движи во насоката во која до сега се движела.
16.7% од времето да се пресметува точно звучи како лоша проценка, но random бројот се генерира секој frame, и со тоа компјутерот останува доста прецизен и покрај оваа мала вредност.
```
if (random.Next(0, 6) == 1)
{
    directionY = Ball.Position.Y - Position.Y;
}
directionX = calculateSideOfBoard();



if (directionX == -1)
{
    Position = Position.MoveToward(new Vector2(Position.X, 390), movementSpeed * (float)delta);
    velocity.Y = Mathf.MoveToward(Velocity.Y, 0, 13);
}
else
{
       if (directionY < 0)
            velocity.Y = movementSpeed * -1;
        else if (directionY > 0)
            velocity.Y = movementSpeed;

}
```
Головите имаат сигнал кој го испраќаат кога топчето ќе влезе во нивниот регион (Area2D). Сигналот го покачува score-от на играчот кој го дал голот. Играчот што прв ќе постигне 3 гола е победник.
## Assets
Играта користи текстури и звуци од веб сајтот [itch.io](https://itch.io) и [kenney.nl](https://kenney.nl), музика од ретро конзолата Sega Genesis.


 ![image_2024-07-07_232407299](https://github.com/gjDime/MorkoPodmorko/assets/172989131/bad3a698-0794-4161-a36e-98b8176fa828) (слика 1.)
![image](https://github.com/gjDime/MorkoPodmorko/assets/172989131/a5f24f0d-45c6-4de8-b551-55461c8747af) (слика 2.)
![image](https://github.com/gjDime/MorkoPodmorko/assets/172989131/8b4ad7f3-f35d-4bf3-902c-b715fefb92a4) (слика 3.)


