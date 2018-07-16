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
        public EventGenerator(string music, Vector2 cameraPosition, Vector2 farmerPosition, int farmerFacing)
        {
            eventMusic = music;
            startingCamPosition = cameraPosition;
            startingFarmerPosition = farmerPosition;
            startFacing = farmerFacing;
            actors = new List<string>();
            commands = new List<string>();
        }

        public void InitiateActor(string name, Vector2 location, int facingDirection)
        {
            actors.Add($"{name} {location.X} {location.Y} {facingDirection}");
        }

        public void AddBigProp(Vector2 location, int objectID)
        {
            commands.Add($"addBigProp {location.X} {location.Y} {objectID}");
        }

        public void AddCookingRecipe(string recipe)
        {
            commands.Add($"addCookingRecipe {recipe}");
        }

        public void AddCraftingRecipe(string recipe)
        {
            commands.Add($"addCraftingRecipe {recipe}");
        }

        //public void AddFloorProp()

        public void AddLantern(int rowInTexture, Vector2 location, int radius)
        {
            commands.Add($"addLantern {rowInTexture} {location.X} {location.Y} {radius}");
        }

        public void AddMail(string mailID)
        {
            commands.Add($"addMailReceived {mailID}");
        }

        public void AddObject(int rowInTexture, Vector2 location)
        {
            commands.Add($"addObject {rowInTexture} {location.X} {location.Y}");
        }

        //public void AddProp()

        public void AddQuest(int questID)
        {
            commands.Add($"addQuest {questID}");
        }

        public void AddTempActor(string character, Vector2 spriteDimensions, Vector2 location, int facing, bool breather=false, string type="Character", string animalName="animal")
        {
            if (type == "Animal") {
                commands.Add($"addTemporaryActor {character} {spriteDimensions.X} {spriteDimensions.Y} {location.X} {location.Y} {facing} {breather} {type} {animalName}");
            }
            else
            {
                commands.Add($"addTemporaryActor {character} {spriteDimensions.X} {spriteDimensions.Y} {location.X} {location.Y} {facing} {breather} {type}");
            }
        }

        //public void AddToTable()

        public void AddTool(string tool)
        {
            commands.Add($"addTool {tool}");
        }

        public void setAmbientLight(int r, int g, int b)
        {
            commands.Add($"ambientLight {r} {g} {b}");
        }

        public void NamingMenu()
        {
            commands.Add($"animalNaming");
        }

        public void Animate(string actor, bool flip, bool loop, int frameDuration, List<int> frames)
        {
            string plusString = "";
            foreach (int frame in frames)
            {
                plusString += " {frame}";
            }
            commands.Add($"animate {actor} {flip} {loop} {frameDuration}"+{plusString});
        }

        public void AttachToTempSprite(string actor)
        {
            commands.Add($"attachCharacterToTempSprite {actor}");
        }

        public void AwardPrize(string type)
        {
            //pan, sculpture, rod, sword, hero, joja, slimeegg
            commands.Add($"awardFestivalPrize {type}");
        }

        //public void bloom()

        public void catQuestion()
        {
            commands.Add($"catQuestion");
        }

        public void MoveFarmer(Vector2 destination, int facingDirection)
        {
            commands.Add($"move farmer {destination.X} {destination.Y} {facingDirection}");
        }

        public string BuildString()
        {
            string initstring = $"{eventMusic}/{startingCamPosition.X} {startingCamPosition.Y}/farmer {startingFarmerPosition.X} {startingFarmerPosition.Y} {startFacing} ";
            return initstring + string.Join(" ", this.actors) + "/" + string.Join("/", this.commands) + "end";
        }
    }
}
