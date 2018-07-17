using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Xna.Framework;

namespace EventGenerator
{
    public class EventGenerator
    {
        string eventMusic;
        Vector2 startingCamPosition;
        Vector2 startingFarmerPosition;
        int startFacing;
        List<string> commands;
        List<string> actors;
        bool skippable;
        bool warpout;

        /// <summary>
        /// Makes a new Event Generator Object
        /// </summary>
        /// <param name="music">The music that is playing at the start of the Event.</param>
        /// <param name="cameraPosition">The position that the camera starts at.</param>
        /// <param name="farmerPosition">The co-ordinates that the farmer should spawn at.</param>
        /// <param name="farmerFacing">The direction the farmer is facing at the start of the Event.</param>
        /// <param name="warpOutOnEnd">The direction the farmer is facing at the start of the Event.</param>
        public EventGenerator(string music, Vector2 cameraPosition, Vector2 farmerPosition, int farmerFacing, bool warpOutOnEnd=false)
        {
            eventMusic = music;
            startingCamPosition = cameraPosition;
            startingFarmerPosition = farmerPosition;
            startFacing = farmerFacing;
            actors = new List<string>();
            commands = new List<string>();
            skippable = false;
            warpout = warpOutOnEnd;
        }

        /// <summary>
        /// Ititiate a new Actor, so that they can be part of the Event.
        /// </summary>
        /// <param name="name">The name of the Actor. For example: "Abigail".</param>
        /// <param name="location">The co-ordinates that this Actor should spawn at.</param>
        /// <param name="facingDirection">The direction they should be facing when they spawn.</param>
        public void InitiateActor(string name, Vector2 location, int facingDirection)
        {
            actors.Add($"{name} {location.X} {location.Y} {facingDirection}");
        }

        /// <summary>
        /// Adds a big prop to the event, used to add items to the event that aren't usually in that map.
        /// </summary>
        /// <param name="location">The co-ordinates that this prop should spawn at.</param>
        /// <param name="objectID">The ID of the object that you are going to spawn.</param>
        public void AddBigProp(Vector2 location, int objectID)
        {
            commands.Add($"addBigProp {location.X} {location.Y} {objectID}");
        }

        /// <summary>
        /// Unlocks a recipe that can be cooked for the player seeing the event.
        /// </summary>
        /// <param name="recipe">The name of the item they are unlocking</param>
        public void AddCookingRecipe(string recipe)
        {
            commands.Add($"addCookingRecipe {recipe}");
        }

        /// <summary>
        /// Unlocks a recipe that can be crafted for the player seeing the event.
        /// </summary>
        /// <param name="recipe">The name of the item they are unlocking</param>
        public void AddCraftingRecipe(string recipe)
        {
            commands.Add($"addCraftingRecipe {recipe}");
        }

        //public void AddFloorProp()

        /// <summary>
        /// Creates a prop lantern that provides light in the defined radius.
        /// </summary>
        /// <param name="rowInTexture">The row in which the lanterns sprite is located.</param>
        /// <param name="location">The co-ordinates that this lantern should spawn at.</param>
        /// <param name="radius">The radius of the lantern's glow.</param>
        public void AddLantern(int rowInTexture, Vector2 location, int radius)
        {
            commands.Add($"addLantern {rowInTexture} {location.X} {location.Y} {radius}");
        }

        /// <summary>
        /// Marks a certain piece of mail as received, even if the player hasn't read it yet or even received it.
        /// </summary>
        /// <param name="mailID">The ID of the mail that is being marked at received.</param>
        public void MarkMailAsReceived(string mailID)
        {
            commands.Add($"addMailReceived {mailID}");
        }

        /// <summary>
        /// Spawns an object that would not usually be in the map at the specified location
        /// </summary>
        /// <param name="rowInTexture">The row in which the objects sprite is located.</param>
        /// <param name="location">The co-ordinates that this object should spawn at.</param>
        public void AddObject(int rowInTexture, Vector2 location)
        {
            commands.Add($"addObject {rowInTexture} {location.X} {location.Y}");
        }

        //public void AddProp()

        /// <summary>
        /// Adds a quest to the players Journal
        /// </summary>
        /// <param name="questID">The ID of the quest to be added to the journal</param>
        public void AddQuest(int questID)
        {
            commands.Add($"addQuest {questID}");
        }

        /// <summary>
        /// Adds a temporary actor to the scene, one that will be deleted later on.
        /// </summary>
        /// <param name="character">The name of the character this TempActor is based on.</param>
        /// <param name="spriteDimensions">The dimensions of the sprite that is drawn on the screen</param>
        /// <param name="location">The co-ordinates that this tempactor should spawn at</param>
        /// <param name="facing">The direction that this tempactor should face when spawning</param>
        /// <param name="breather">Does this character have the breathing effect?</param>
        /// <param name="type">Must be either "Character", "Monster" or "Animal"</param>
        /// <param name="animalName">Only needed if type="Animal", sets the name of the tempactor</param>
        public void AddTempActor(string character, Vector2 spriteDimensions, Vector2 location, int facing, bool breather=false, string type="Character", string animalName="animal")
        {
            if (type == "Animal") {
                commands.Add($"addTemporaryActor {character} {spriteDimensions.X} {spriteDimensions.Y} {location.X} {location.Y} {facing} {breather.ToString().ToLower()} {type} {animalName}");
            }
            else
            {
                commands.Add($"addTemporaryActor {character} {spriteDimensions.X} {spriteDimensions.Y} {location.X} {location.Y} {facing} {breather.ToString().ToLower()} {type}");
            }
        }

        //public void AddToTable()

        /// <summary>
        /// Sets the player to be holding one of two tools
        /// </summary>
        /// <param name="tool">Must be either "Sword" or "Wand"</param>
        public void AddTool(string tool)
        {
            commands.Add($"addTool {tool}");
        }

        /// <summary>
        /// Sets the ambient lighting of the event, for creating a more atmospheric situation.
        /// </summary>
        /// <param name="r">The r value of the RGB colour</param>
        /// <param name="g">The g value of the RGB colour</param>
        /// <param name="b">The b value of the RGB colour</param>
        public void SetAmbientLight(int r, int g, int b)
        {
            commands.Add($"ambientLight {r} {g} {b}");
        }

        /// <summary>
        /// Brings up the Animal Naming menu, no idea why you'd need this
        /// </summary>
        public void NamingMenu()
        {
            commands.Add($"animalNaming");
        }

        /// <summary>
        /// Animates an actor for a certain duration using specified frames
        /// </summary>
        /// <param name="actor">The name of the actor being animated</param>
        /// <param name="flip">Is this animation flipped?</param>
        /// <param name="loop">Is this animation looped? If not the animation will end after all the frames are done</param>
        /// <param name="frameDuration">The duration of each frame</param>
        /// <param name="frames">A list of frames that will be cycled through when showing the animation</param>
        public void Animate(string actor, bool flip, bool loop, int frameDuration, List<int> frames)
        {
            string plusString = "";
            foreach (int frame in frames)
            {
                plusString += $" {frame}";
            }
            commands.Add($"animate {actor} {flip.ToString().ToLower()} {loop.ToString().ToLower()} {frameDuration}"+plusString);
        }

        /// <summary>
        /// Attaches an actor to the most recently called tempsprite
        /// </summary>
        /// <param name="actor">The name of the actor to attach the tempsprite too</param>
        public void AttachToTempSprite(string actor)
        {
            commands.Add($"attachCharacterToTempSprite {actor}");
        }

        /// <summary>
        /// Awards a prize, but it must be of a specific type.
        /// </summary>
        /// <param name="type">Possible prize types are "pan", "sculpture", "rod", "sword", "hero", "joja", and "slimeegg"</param>
        public void AwardPrize(string type)
        {
            //pan, sculpture, rod, sword, hero, joja, slimeegg
            commands.Add($"awardFestivalPrize {type}");
        }

        //public void bloom()

        /// <summary>
        /// Asks the adoption question for when you receive your cat/dog. No idea why you would need this.
        /// </summary>
        public void CatQuestion()
        {
            commands.Add($"catQuestion");
        }

        /// <summary>
        /// Triggers Demetrius' cave question, about whether you want Fruit Bats or Mushroom Farm. No idea why you would need this.
        /// </summary>
        public void Cave()
        {
            commands.Add($"cave");
        }

        /// <summary>
        /// Changes to a different map, and continues the event there instead
        /// </summary>
        /// <param name="location">The map to change location to</param>
        public void ChangeLocation(string location)
        {
            commands.Add($"changeLocation {location}");
        }

        /// <summary>
        /// Changes a tile on a map for the purpose of the event
        /// </summary>
        /// <param name="layerName">The name of the layer to change the tile on. Can be "Back", "Buildings", "Paths", "Front", "AlwaysFront"</param>
        /// <param name="location">The co-ordinates of the tile that is being changed</param>
        /// <param name="tileID">The ID of the new tile that is being placed at this location</param>
        public void ChangeMapTile(string layerName, Vector2 location, int tileID)
        {
            commands.Add($"changeMapTile {layerName} {location.X} {location.Y} {tileID}");
        }

        /// <summary>
        /// Changes the portait of the NPC to the portrait specified
        /// </summary>
        /// <param name="npcName">The name of the NPC to change the portrait of</param>
        /// <param name="portrait">The portrait that it is being changed to</param>
        public void ChangePortrait(string npcName, string portrait)
        {
            commands.Add($"changePortrait {npcName} {portrait}");
        }

        /// <summary>
        /// Changes the sprite of the actor, used for clothes being changed, etc
        /// </summary>
        /// <param name="actor">The actor that is having it's sprite changed</param>
        /// <param name="sprite">The sprite that it is being changed to</param>
        public void ChangeSprite(string actor, string sprite)
        {
            commands.Add($"changeSprite {actor} {sprite}");
        }

        /// <summary>
        /// Changes to a map temporarily, and can pan it if told to
        /// </summary>
        /// <param name="mapName">The name of the map to temporarily change to</param>
        /// <param name="pan">Should the camera pan across it?</param>
        public void ChangeToTempMap(string mapName, bool pan)
        {
            commands.Add($"changeToTemporaryMap {mapName} {pan.ToString().ToLower()}");
        }

        /// <summary>
        /// Changes the Y offset of the specified NPC
        /// </summary>
        /// <param name="npcName">The name of the NPC to change the offset of</param>
        /// <param name="offset">The amount to offset their Y position by. Can be negative to go upwards</param>
        public void ChangeYOffset(string npcName, int offset)
        {
            commands.Add($"changeYSourceRectOffset {npcName} {offset}");
        }

        /// <summary>
        /// Brings up the character selection menu. No idea why you would need this.
        /// </summary>
        public void SelectCharacter()
        {
            commands.Add($"characterSelect");
        }

        /// <summary>
        /// Plays a specified cutscene during the event.
        /// </summary>
        /// <param name="cutscene">The name of the cutscene to play</param>
        public void PlayCutscene(string cutscene)
        {
            commands.Add($"cutscene {cutscene}");
        }

        /// <summary>
        /// Performs the action of right-clicking at the location specified
        /// </summary>
        /// <param name="location">The co-ordinates at which the action is to be performed</param>
        public void DoAction(Vector2 location)
        {
            commands.Add($"doAction {location.X} {location.Y}");
        }

        /// <summary>
        /// Not even gonna lie... no idea what this does at all
        /// </summary>
        public void ElliotBookTalk()
        {
            commands.Add($"elliotbooktalk");
        }

        /// <summary>
        /// Makes the specified actor perform an emote (the white bubbles with pictures in)
        /// </summary>
        /// <param name="actor">The actor that will perform the emote</param>
        /// <param name="emoteID">The ID of the emote to perform</param>
        public void DoEmote(string actor, int emoteID)
        {
            commands.Add($"emote {actor} {emoteID}");
        }

        /// <summary>
        /// Resets the sprite of an actor to it's original state
        /// </summary>
        /// <param name="actor">Name of the actor to reset visually</param>
        public void ResetSprite(string actor)
        {
            commands.Add($"extendSourceRect {actor} reset");
        }

        /// <summary>
        /// Function to make the character blink
        /// </summary>
        /// <param name="eyes">0 or 1, 0 for open, 1 for closed. [Citation Needed]</param>
        /// <param name="blink">A negative number but I don't know what it does.</param>
        public void ChangeEyes(int eyes, int blink)
        {
            commands.Add($"eyes {eyes} {blink}");
        }

        /// <summary>
        /// Makes the specified actor face a certain direction without moving
        /// </summary>
        /// <param name="actor">The actor to perform this action on</param>
        /// <param name="direction">The direction the actor will face</param>
        /// <param name="pause">Should the game pause after doing this?</param>
        public void FaceDirection(string actor, int direction, bool pause=false)
        {
            commands.Add($"faceDirection {actor} {direction} {(!pause).ToString().ToLower()}");
        }

        /// <summary>
        /// Makes the screen fade, only use at the end of an event
        /// </summary>
        public void Fade()
        {
            commands.Add($"fade");
        }

        /// <summary>
        /// Sets the animation of the farmer to a specified value
        /// </summary>
        /// <param name="animationID">ID of the animation to apply to the farmer</param>
        public void SetFarmerAnimation(int animationID)
        {
            commands.Add($"farmerAnimation {animationID}");
        }

        /// <summary>
        /// Makes the farmer eat a specified object. It can be anything, even inedibles.
        /// </summary>
        /// <param name="objectID">The ID of the item that the farmer will eat</param>
        public void FarmerEat(int objectID)
        {
            commands.Add($"farmerEat {objectID}");
        }

        /// <summary>
        /// Brings in another event string, these ones have string IDs, not integer IDs.
        /// </summary>
        /// <param name="forkID">The key of the event string you are forking</param>
        public void Fork(string forkID)
        {
            commands.Add($"fork {forkID}");
        }

        /// <summary>
        /// Adds the amount of friendship points to the specified NPC
        /// </summary>
        /// <param name="npc">The name of the NPC to add the points to</param>
        /// <param name="amount">The amount of friendship points to add. 250 is 1 heart</param>
        public void IncreaseFriendship(string npc, int amount)
        {
            commands.Add($"friendship {npc} {amount}");
        }

        /// <summary>
        /// Make the screen fade. Used during an event. At the end use Fade() instead
        /// </summary>
        /// <param name="speed">The speed at which to fade to black</param>
        public void GlobalFade(int speed)
        {
            commands.Add($"globalFade {speed}");
        }

        /// <summary>
        /// Make the screen unfade. You cannot use this if you used Fade(), use GlobalFade() instead
        /// </summary>
        /// <param name="speed">The speed at which to clear the screen</param>
        public void GlobalUnfade(int speed)
        {
            commands.Add($"globalFadeToClear {speed}");
        }

        /// <summary>
        /// Makes the screen glow
        /// </summary>
        /// <param name="r">The r value of the RGB colour</param>
        /// <param name="g">The g value of the RGB colour</param>
        /// <param name="b">The b value of the RGB colour</param>
        /// <param name="hold">Should this glow persist or slowly fade?</param>
        public void Glow(int r, int g, int b, bool hold)
        {
            commands.Add($"glow {r} {g} {b} {hold.ToString().ToLower()}");
        }

        /// <summary>
        /// Makes the player hold an object
        /// </summary>
        /// <param name="objectID">The ID of the object to make the player hold</param>
        public void GrabObject(int objectID)
        {
            commands.Add($"grabObject {objectID}");
        }

        /// <summary>
        /// No idea why you'd need this
        /// </summary>
        public void GrandpaCandles()
        {
            commands.Add($"grandpaCandles");
        }

        /// <summary>
        /// This is Grandpa's Initial Evaluation
        /// </summary>
        public void GrandpaEvaluation()
        {
            commands.Add($"grandpaEvaluation");
        }

        /// <summary>
        /// This is the manual one you can trigger yourself
        /// </summary>
        public void GrandpaEvaluation2()
        {
            commands.Add($"grandpaEvaluation2");
        }

        /// <summary>
        /// Makes everyone stop
        /// </summary>
        public void HaltEveryone()
        {
            commands.Add($"halt");
        }

        /// <summary>
        /// You died but science saved you
        /// </summary>
        public void HospitalDeath()
        {
            commands.Add($"hospitaldeath");
        }

        /// <summary>
        /// Makes the farmer hold an item above their head
        /// </summary>
        /// <param name="type">Valid types are "pan", "hero", "sculpture", "joja", "slimeEgg", "rod", "sword" or "ore"</param>
        public void FarmerHoldAboveHead(string type)
        {
            commands.Add($"itemAboveHead {type}");
        }

        /// <summary>
        /// Makes the specified actor jump
        /// </summary>
        /// <param name="actor">The actor that is going to be doing the jumping</param>
        /// <param name="intensity">The intensity at which to make them jump</param>
        public void Jump(string actor, int intensity=8)
        {
            commands.Add($"jump {actor} {intensity}");
        }

        /// <summary>
        /// Sends a piece of mail to the players mailbox, that will arrive the next morning
        /// </summary>
        /// <param name="mailID">The ID of the mail that the player is going to recieve</param>
        public void SendMail(string mailID)
        {
            commands.Add($"mail {mailID}");
        }

        /// <summary>
        /// Shows a message with no portrait or options
        /// </summary>
        /// <param name="text">The message that is going to be displayed</param>
        public void ShowMessage(string text)
        {
            commands.Add($"message \\\"{text}\\\"");
        }

        /// <summary>
        /// You don't need this... like, at all
        /// </summary>
        public void MineDeath()
        {
            commands.Add($"minedeath");
        }

        /// <summary>
        /// Makes that specified actor more to a destination
        /// </summary>
        /// <param name="actor">The actor to move to the destination</param>
        /// <param name="destination">The co-ordinates that the actor is going to move to</param>
        /// <param name="facing">The way the actor will face when they arrive at their destination</param>
        /// <param name="pause">Should the game pause after the actor arrives?</param>
        public void MoveActor(string actor, Vector2 destination, int facing, bool pause=false)
        {
            commands.Add($"move {actors} {destination.X} {destination.Y} {facing} {(!pause).ToString().ToLower()}");
        }

        /// <summary>
        /// Pause for a set duration, in milliseconds
        /// </summary>
        /// <param name="duration">Duration to pause in milliseconds</param>
        public void Pause(int duration)
        {
            commands.Add($"pause {duration}");
        }

        //public void PixelZoom()

        /// <summary>
        /// Makes music play, but shouldn't be triggered at the start of an event.
        /// </summary>
        /// <param name="track">The name of the music track to play</param>
        public void PlayMusic(string track)
        {
            commands.Add($"playMusic {track}");
        }

        /// <summary>
        /// Use this function to play sound effects, to simulate stuff happening
        /// </summary>
        /// <param name="sound">The name of the sound to play</param>
        public void PlaySound(string sound)
        {
            commands.Add($"playSound {sound}");
        }

        /// <summary>
        /// Gives back control to the player. This shouldn't be used to end an event, this API does that automatically
        /// </summary>
        public void GiveBackControl()
        {
            commands.Add($"playerControl");
        }

        /// <summary>
        /// Offsets the specified actor by a certain position. Use this to change where they snap to the grid
        /// </summary>
        /// <param name="actor">The actor to change the offset for</param>
        /// <param name="offset">The vector that defines how much to change on each axis</param>
        public void OffsetActor(string actor, Vector2 offset)
        {
            commands.Add($"positionOffset {actor} {offset.X} {offset.Y}");
        }

        /// <summary>
        /// Totally honest, I have no idea... Wiki says nothing and it's never used in-game
        /// </summary>
        /// <param name="actor">The actor to do... whatever this does</param>
        public void ProceedPosition(string actor)
        {
            commands.Add($"proceedPosition {actor}");
        }

        /// <summary>
        /// Ask a question to the player with no immediate outcome, but the outcomes can be called later on, see CharacterSplitSpeak()
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <param name="answer1">The first answer</param>
        /// <param name="answer2">The second answer</param>
        public void NullQuestion(string question, string answer1, string answer2)
        {
            commands.Add($"question null \"{question}#{answer1}#{answer2}\"");
        }

        /// <summary>
        /// Ask a question and choose different paths based on this. This should have a Fork() function directly after it.
        /// </summary>
        /// <param name="question">The question to ask</param>
        /// <param name="fork">The word fork, followed by the option number that will perform the Fork action. Example: fork1 in a question with 3 options, will make the second option take you to a different event string, while the first and third will continue the current one</param>
        /// <param name="answers">The list of answers the player can select from</param>
        public void Question(string question, string fork, List<string> answers)
        {
            string plusString = "";
            foreach (string answer in answers)
            {
                plusString += $"#{answer}";
            }
            commands.Add($"question {fork} \"{question}{plusString}\"");
        }

        /// <summary>
        /// Removes an item from the players inventory
        /// </summary>
        /// <param name="objectID">The ID of the item to remove. It only removes one</param>
        public void RemoveItem(int objectID)
        {
            commands.Add($"removeItem {objectID}");
        }

        /// <summary>
        /// Removes the object at the specified location
        /// </summary>
        /// <param name="location">The co-ordinates of the object to remove</param>
        public void RemoveObject(Vector2 location)
        {
            commands.Add($"removeObject {location.X} {location.Y}");
        }

        /// <summary>
        /// Removes a quest from the players Journal
        /// </summary>
        /// <param name="questID">The ID of the quest to remove from the journal</param>
        public void RemoveQuest(int questID)
        {
            commands.Add($"removeQuest {questID}");
        }

        /// <summary>
        /// Removes a sprite from a specified location
        /// </summary>
        /// <param name="location">The co-ordinates of the sprite to remove</param>
        public void RemoveSprite(Vector2 location)
        {
            commands.Add($"removeSprite {location.X} {location.Y}");
        }

        /// <summary>
        /// Remove all temporary sprites from the event
        /// </summary>
        public void RemoveTempSprites()
        {
            commands.Add($"removeTemporarySprites");
        }

        /// <summary>
        /// Removes a tile at a specified location on a certain layer
        /// </summary>
        /// <param name="location">The location at which to remove the tile</param>
        /// <param name="layer">The name of the layer to change the tile on. Can be "Back", "Buildings", "Paths", "Front", "AlwaysFront"</param>
        public void RemoveTile(Vector2 location, int layer)
        {
            commands.Add($"removeTile {location.X} {location.Y} {layer}");
        }

        /// <summary>
        /// Resets the variables
        /// </summary>
        public void ResetVariable()
        {
            commands.Add($"resetVariable");
        }

        /// <summary>
        /// Gives the rusty key to the player
        /// </summary>
        public void GiveRustyKey()
        {
            commands.Add($"rustyKey");
        }

        /// <summary>
        /// Flashes the screen with the set opacity
        /// </summary>
        /// <param name="alpha">A value ranging from 0 to 1.0</param>
        public void Flash(float alpha)
        {
            commands.Add($"screenFlash {alpha}");
        }

        /// <summary>
        /// Sets the player to running
        /// </summary>
        public void SetRunning()
        {
            commands.Add($"setRunning");
        }

        /// <summary>
        /// Makes the actor shake for a given duration
        /// </summary>
        /// <param name="actor">The specific actor to make shake</param>
        /// <param name="duration">The duration in milliseconds for them to shake for</param>
        public void Shake(string actor, int duration)
        {
            commands.Add($"shake {actor} {duration}");
        }

        /// <summary>
        /// Flips the farmer along the Y axis
        /// </summary>
        public void FlipFarmer()
        {
            commands.Add($"showFrame farmer flip");
        }

        /// <summary>
        /// Show the frame of the specified actor
        /// </summary>
        /// <param name="actor">The actor to show the frame of</param>
        /// <param name="frameID">The ID of the frame to apply to the actor</param>
        public void ShowFrame(string actor, int frameID)
        {
            commands.Add($"showFrame {actor} {frameID}");
        }

        //public void ShowRivalFrame()

        /// <summary>
        /// Only call this function if you want your event to be skippable
        /// </summary>
        public void Skippable()
        {
            skippable = true;
        }

        /// <summary>
        /// Makes a specified character speak
        /// </summary>
        /// <param name="character">The character to say the message, effects the portrait</param>
        /// <param name="text">The message that the character says</param>
        public void CharacterSpeak(string character, string text)
        {
            commands.Add($"speak {character} \\\"{text}\\\"");
        }

        /// <summary>
        /// Sets the specific temporary sprite
        /// </summary>
        /// <param name="sprite">The name of the sprite to make the specific temp sprite</param>
        public void SpecificTempSprite(string sprite)
        {
            commands.Add($"specificTemporarySprite {sprite}");
        }

        /// <summary>
        /// Sets the speed of the farmer by a modifier
        /// </summary>
        /// <param name="modifier">The modifier to multiple the farmers speed by</param>
        public void SetFarmerSpeed(int modifier)
        {
            commands.Add($"speed farmer {modifier}");
        }

        /// <summary>
        /// Sets the speed of an actor
        /// </summary>
        /// <param name="actor">The actor to set the speed of</param>
        /// <param name="speed">The value to set the actors speed to</param>
        public void SetActorSpeed(string actor, int speed)
        {
            commands.Add($"speed {actor} {speed}");
        }

        public void CharacterSplitSpeak(string actor, List<string> responses)
        {
            string plusString = "";
            foreach (string response in responses)
            {
                plusString += $"{response}~";
            }
            commands.Add($"splitSpeak {actor} \\\"{plusString.Remove(plusString.Length-1)}\\\"");
        }

        /// <summary>
        /// Makes the farmer start to Jitter
        /// </summary>
        public void Jitter()
        {
            commands.Add($"startJittering");
        }

        //public void StopAdvancedMovement()

        /// <summary>
        /// Stops the animation the farmer is currently running
        /// </summary>
        public void StopFarmerAnimation()
        {
            commands.Add($"stopAnimation farmer");
        }

        /// <summary>
        /// Stops the animation that the actor specified is currently running
        /// </summary>
        /// <param name="actor">The name of the actor to stop the animation of</param>
        /// <param name="frame">The frame to stop the animation on</param>
        public void StopAnimation(string actor, int frame)
        {
            commands.Add($"stopAnimation {actor} {frame}");
        }

        /// <summary>
        /// Stops the screen from glowing
        /// </summary>
        public void StopGlowing()
        {
            commands.Add($"stopGlowing");
        }

        /// <summary>
        /// Stops the player's jittering
        /// </summary>
        public void StopJittering()
        {
            commands.Add($"stopJittering");
        }

        /// <summary>
        /// Stops the music that is currently playing. This does not work for sounds
        /// </summary>
        public void StopMusic()
        {
            commands.Add($"stopMusic");
        }

        /// <summary>
        /// Makes the player stop running
        /// </summary>
        public void StopRunning()
        {
            commands.Add($"stopRunning");
        }

        /// <summary>
        /// Stop the specified actor swimming
        /// </summary>
        /// <param name="actor">The actor to stop swimming</param>
        public void StopSwimming(string actor)
        {
            commands.Add($"stopSwimming {actor}");
        }

        /// <summary>
        /// Make the specified actor swim
        /// </summary>
        /// <param name="actor">The actor to make swim</param>
        public void StartSwimming(string actor)
        {
            commands.Add($"startSwimming {actor}");
        }

        /// <summary>
        /// Switches the event to another event, triggering a different event string
        /// </summary>
        /// <param name="eventID">The ID of the event to trigger</param>
        public void ChangeEvent(int eventID)
        {
            commands.Add($"switchEvent {eventID}");
        }

        /// <summary>
        /// This has no effect in game
        /// </summary>
        /// <param name="actor"></param>
        public void TaxVote(string actor)
        {
            commands.Add($"taxvote");
        }

        /// <summary>
        /// Creates a temporary sprite that can be placed at a certain location
        /// </summary>
        /// <param name="location">The location in which to create the sprite</param>
        /// <param name="rowInTexture">The row in the the texture that the sprite belongs</param>
        /// <param name="animationLength">The length of the animation</param>
        /// <param name="animationInterval">The interval between each frame</param>
        /// <param name="flipped">Whether or not the sprite is flipped</param>
        /// <param name="loopCount">The amount of times the animation should loop through</param>
        public void TemporarySprite(Vector2 location, int rowInTexture, int animationLength, int animationInterval, bool flipped, int loopCount)
        {
            commands.Add($"temporarySprite {location.X} {location.Y} {animationLength} {animationInterval} {flipped.ToString().ToLower()} {loopCount}");
        }

        /// <summary>
        /// Shows text above the head of the specified actor
        /// </summary>
        /// <param name="actor">The actor to show text above</param>
        /// <param name="text">The text to display above the actor. Keep it short</param>
        public void ShowTextAbove(string actor, string text)
        {
            commands.Add($"textAboveHead {actor} \\\"{text}\\\"");
        }

        /// <summary>
        /// This does nothing that you need to use really
        /// </summary>
        public void TutorialMenu()
        {
            commands.Add($"tutorialMenu");
        }

        /// <summary>
        /// Updates the minigame currently being played
        /// </summary>
        /// <param name="eventData">The event data to update the minigame too</param>
        public void UpdateMinigame(int eventData)
        {
            commands.Add($"updateMinigame {eventData}");
        }

        /// <summary>
        /// Pans the viewport across the screen. Speed is determined by how long you want it to last
        /// </summary>
        /// <param name="destination">The destination co-ordinates for the panning</param>
        /// <param name="duration">How long the panning animation is going to last</param>
        public void PanViewport(Vector2 destination, int duration)
        {
            commands.Add($"viewport move {destination.X} {destination.Y} {duration}");
        }

        /// <summary>
        /// Instantly set the viewport to a new location
        /// </summary>
        /// <param name="destination">The co-ordinates to set the viewport to</param>
        public void MoveViewport(Vector2 destination)
        {
            commands.Add($"viewport {destination.X} {destination.Y}");
        }

        //public void WaitFor()

        /// <summary>
        /// Warp an actor instantly to a new location
        /// </summary>
        /// <param name="actor">The actor to warp to the location</param>
        /// <param name="destination">The co-ordinates to warp the actor to</param>
        public void WarpActor(string actor, Vector2 destination)
        {
            commands.Add($"warp {actor} {destination.X} {destination.Y}");
        }

        /// <summary>
        /// Basically useless in an event
        /// </summary>
        /// <param name="frame">The frame to set the wedding sprites to</param>
        public void WeddingSprite(int frame)
        {
            commands.Add($"weddingSprite {frame}");
        }

        /// <summary>
        /// Makes the farmer walk to a new location
        /// </summary>
        /// <param name="destination">The co-ordinates to send the farmer to</param>
        /// <param name="facingDirection">The direction for the farmer to face when they arrive</param>
        public void MoveFarmer(Vector2 destination, int facingDirection)
        {
            commands.Add($"move farmer {destination.X} {destination.Y} {facingDirection}");
        }

        /// <summary>
        /// Builds all the inputted commands into a string that is readable by Stardew Valley as an Event String
        /// </summary>
        /// <returns></returns>
        public string BuildString()
        {
            string initstring = $"{eventMusic}/{startingCamPosition.X} {startingCamPosition.Y}/farmer {startingFarmerPosition.X} {startingFarmerPosition.Y} {startFacing} ";
            return initstring + string.Join(" ", this.actors) + $"/{(skippable?"":"skippable/")}" + string.Join("/", this.commands) + (warpout?"end warpOut":"end");
        }
    }
}
