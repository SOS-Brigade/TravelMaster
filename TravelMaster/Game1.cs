﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame_Tools.CharacterLogic;
using MonoGame_Tools.Conversation;
using System.Collections.Generic;
using MonoGame_Tools.Fundamental;
using MonoGame_Tools.Items;
using System;

namespace TravelMaster
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont Segoe14;
        TextBox newTextBox;
        TextBox newTextBox2;
        Texture2D Test2D;
        Conversation testConv;
        ConversationController conversationController;
        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;
        MouseState oldMouse;
        MouseState newMouse;
        Vector2 MouseLocation;
        Texture2D test1;
        Texture2D test2;
        Character DefaultChar;
        List<Character> PlayerCharacters;
        List<Character> EnemyCharacters;
        List<Character> NeutralCharacters;
        CharacterController characterController;
        SituationController situationController;
        CharacterMenu CharacterMenu;
        Character TraderTest;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Constants.MainWindowWidth;
            graphics.PreferredBackBufferHeight = Constants.MainWindowHeight;

            Mouse.WindowHandle = Window.Handle;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Set mouse visable.
            this.IsMouseVisible = true;

            conversationController = new ConversationController();
            conversationController.loadFromLibrary(1, Content, spriteBatch);

            oldKeyboardState = Keyboard.GetState();
            oldMouse = Mouse.GetState();

            MouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            Character DefaultCharacter = new Character(Content, (int)Constants.CharacterType.Basic);
            Character Steve = new Character(Content, (int)Constants.CharacterType.Flyer);
            Character Stan = new Character(Content, (int)Constants.CharacterType.Horse);

            Stan.CharacterController = (int)Constants.CharacterControllerType.Enemy;
            PlayerCharacters = new List<Character>();
            PlayerCharacters.Add(Steve);
            PlayerCharacters.Add(Stan);

            characterController = new CharacterController();
            characterController.addCharacter(DefaultCharacter);
            characterController.addCharacter(Steve);
            characterController.addCharacter(Stan);
            characterController.moveCharacter(Steve, 2, 2);
            characterController.moveCharacter(Stan, 1, 1);
            characterController.moveCharacter(DefaultCharacter, 0, 0);

            situationController = new SituationController();
            //situationController.loadSituation(0, mapController, characterController, conversationController, Content, spriteBatch);
            List<Texture2D> temp = new List<Texture2D>();
            temp.Add(this.Content.Load<Texture2D>("top32"));
            temp.Add(this.Content.Load<Texture2D>("mid32"));
            temp.Add(this.Content.Load<Texture2D>("bot32"));

            CharacterMenu = new CharacterMenu(temp, this.Content.Load<SpriteFont>("segoe"));

            ////Trader = new Character(Content, (int)Constants.CharacterController.Neutral);
            //ItemLogic.giveTo(Trader, new Item("Steve Spear", 1, 30, (int)Constants.ItemSlot.Weapon));
            //Trader.Items[0].ItemModifier.Mana = 400;
            //ItemLogic.giveTo(Trader, new Item("Steve Stick", 1, 30, (int)Constants.ItemSlot.Weapon));
            //ItemLogic.giveTo(Trader, new Item("Steve Legsicles", 1, 30, (int)Constants.ItemSlot.Legs));
            //ItemLogic.giveTo(Trader, new Item("Steve Chesticles", 1, 30, (int)Constants.ItemSlot.Chest));
            //ItemLogic.giveTo(Trader, new Item("Health Potion", 30, 30, (int)Constants.ItemSlot.Consumable));
            //ItemLogic.giveTo(Trader, new Item("Health Potion", 2, 30, (int)Constants.ItemSlot.Consumable));
            //ItemLogic.giveTo(Trader, new Item("Hurt Potion", 100, 30, (int)Constants.ItemSlot.Consumable));

            //ItemLogic.giveToFrom(characterController.AllCharacters[0], Trader, Trader.Items[4]);
            //ItemLogic.giveToFrom(characterController.AllCharacters[0], Trader, Trader.Items[4]);
            //ItemLogic.giveToFrom(characterController.AllCharacters[0], Trader, Trader.Items[4]);
            ////ItemLogic.equipItem(characterController.AllCharacters[0], characterController.AllCharacters[0].Items[0]);

            //Console.WriteLine(Trader.listInventory());

            //Console.WriteLine(characterController.AllCharacters[0].listInventory());

            //Console.WriteLine(characterController.AllCharacters[0].TotalModifier.Mana);
            // TODO: use this.Content to load your game content here

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            newMouse = Mouse.GetState();
            newKeyboardState = Keyboard.GetState();

            #region input

            if (newKeyboardState.IsKeyDown(Keys.S)) //testing script
            {
                if (!oldKeyboardState.IsKeyDown(Keys.S))
                {
                    //    conversationController.startConv("help");
                    characterController.AllCharacters.Clear();
                }
            }
            else if (oldKeyboardState.IsKeyDown(Keys.S))
            {
                //nothing
            }


            if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                int MapRow = (int)(MouseLocation.X / Constants.MapSquareSize);
                int MapCol = (int)(MouseLocation.Y / Constants.MapSquareSize);
                //IsDisplayingMoveRange = false;

                /*
                foreach (Character c in PlayerCharacters)  
                {
                    if (Movement.testInputIsWithinMapBounds(MapRow, MapCol, mapController.Maps.ElementAt(mapController.CurrentMap)))
                    {
                        if (c.HasMove && c.Location.X == MapRow && c.Location.Y == MapCol)
                        {
                            Movement.calculateMovementRange((int)c.Location.X, (int)c.Location.Y, c.TotalModifier.MoveRange, mapController.Maps.ElementAt(mapController.CurrentMap), c);
                            IsDisplayingMoveRange = true;
                            selectCharacter(c);
                            break;
                        }
                        else if (c.Selected && IsDisplayingMoveRange && !isCharacterAt(MapRow, MapCol) && mapController.Maps.ElementAt(mapController.CurrentMap).map2DArray[MapRow][MapCol].inRangeFlag == 1)
                        {
                            c.moveToLocation(MapRow, MapCol);
                            IsDisplayingMoveRange = false;
                            c.Selected = false;
                            c.HasMove = false;
                            break;
                        }
                        else if (isCharacterAt(MapRow, MapCol) || mapController.Maps.ElementAt(mapController.CurrentMap).map2DArray[MapRow][MapCol].inRangeFlag != 1)
                        {
                            IsDisplayingMoveRange = false;
                        }
                    }
                }*/
                //if (situationController.AllowCharacterMovement)
                //{ .movementInput(situationController, mapController, characterController, MapRow, MapCol, mapController.getCurrentMap(), ref IsDisplayingMoveRange); }
            }


            #endregion
            /*if (turnCompleted())
            {
                nextTurn();
            }*/
            //turn logic
            //situationController.inputLogic(mapController, characterController, conversationController, Content, spriteBatch);
            situationController.tryNextTurn(characterController);
            CharacterMenu.Input(characterController.AllCharacters, oldMouse);


            /*if (InputMethods.checkIfMouseClickInBounds(OldMouse, new Vector2(0, 0), new Vector2(100, 100), (int)Constants.MouseButtons.Middle))
            {
                Console.Write("youpressedit");
            }*/

            oldKeyboardState = newKeyboardState; //necissary for functionality
            oldMouse = newMouse;

            // TODO: Add your update logic here
            //testConv.start();
            //testConv.input();
            //ConvController.Input();
            //conversationController.Input();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();


            //spriteBatch.DrawString(Segoe14, "Dick Butt", new Vector2(0, 0), Color.Black);
            // TODO: Add your drawing code here
            Texture2D DefaultTexture2D = Content.Load<Texture2D>("TextBoxDefault");
            newTextBox = new TextBox(DefaultTexture2D, Segoe14, "Test", "Test");
            newTextBox.Draw(spriteBatch, new Rectangle(0, 500, 1600, 400), new Vector2(55, 565), new Vector2(65, 645));
            testConv.draw(spriteBatch);
            newTextBox.Draw(spriteBatch);

            //HerbMap.Draw(spriteBatch, LandTextures);
            //mapController.Draw(spriteBatch, LandTextures);
            foreach (Character c in PlayerCharacters)
            {
                c.Draw(spriteBatch);
            }

           //situationController.Draw(spriteBatch, conversationController, characterController, mapController, LandTextures);
           //if (IsDisplayingMoveRange)
           //{
           //    Movement.Draw(spriteBatch, mapController.Maps.ElementAt(mapController.CurrentMap), Blue, Yellow);
           //}
           //Movement.Draw(spriteBatch, mapController.Maps.ElementAt(mapController.CurrentMap), Blue, Yellow);
           //characterController.Draw(spriteBatch);
           //conversationController.Draw(spriteBatch, graphics.GraphicsDevice);
           spriteBatch.DrawString(this.Content.Load<SpriteFont>("segoe"), "X: " + /*(int)(MouseLocation.X / Constants.MapSquareSize) Mouse.GetState().X + " Y: " + /*(int)(MouseLocation.Y / Constants.MapSquareSize)*/ MouseLocation.Y, new Vector2(0, 0), Color.White);

            //spriteBatch.Draw(this.Content.Load<Texture2D>("Wheel"), new Vector2(128, 128), Color.White);


            /*spriteBatch.Draw(this.Content.Load<Texture2D>("top32"), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("Segoe14"), "Attack", new Vector2(110, 108), Color.White);
            spriteBatch.Draw(this.Content.Load<Texture2D>("mid32"), new Vector2(100, 132), Color.White);
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("Segoe14"), "Move", new Vector2(110, 140), Color.White);
            spriteBatch.Draw(this.Content.Load<Texture2D>("mid32"), new Vector2(100, 164), Color.White);
            spriteBatch.DrawString(this.Content.Load<SpriteFont>("Segoe14"), "Items", new Vector2(110, 172), Color.White);
            spriteBatch.Draw(this.Content.Load<Texture2D>("mid32"), new Vector2(100, 196), Color.White);
            spriteBatch.Draw(this.Content.Load<Texture2D>("bot32"), new Vector2(100, 228), Color.White);
            */
            CharacterMenu.Draw(spriteBatch, characterController.AllCharacters);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        //        /*  public void selectCharacter(Character target)
        //          {
        //              foreach (Character c in PlayerCharacters)
        //              {
        //                  if (c == target)
        //                  {
        //                      c.Selected = true;
        //                  }
        //                  else
        //                  {
        //                      c.Selected = false;
        //                  }
        //              }
        //          }

        //          public Boolean isCharacterAt(int x, int y)
        //          {
        //              foreach (Character c in PlayerCharacters)
        //              {
        //                  if ((int)c.Location.X == x && (int)c.Location.Y == y)
        //                  {
        //                      return true;
        //                  }
        //              }
        //              return false;
        //          }
        //          */

        //        public Boolean turnCompleted()
        //        {
        //            foreach (Character c in characterController.AllCharacters)
        //            {
        //                if (c.HasMove)
        //                {
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }

        //        public void nextTurn()
        //        {
        //            foreach (Character c in characterController.AllCharacters)
        //            {
        //                c.HasMove = true;
        //                c.HasAction = true;
        //            }
        //        }
    }
}
