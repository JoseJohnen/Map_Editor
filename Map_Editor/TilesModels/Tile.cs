using Interfaz.Utilities;
using Map_Editor_HoD.Assistants;
using Map_Editor_HoD.Code.Models;
using Map_Editor_HoD.Controllers;
using Map_Editor_HoD.FurnitureModels;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Graphics;
using Stride.Physics;
using Stride.Rendering.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using Quaternion = Stride.Core.Mathematics.Quaternion;
using UtilityAssistant = Interfaz.Utilities.UtilityAssistant;
using Vector3 = System.Numerics.Vector3;

namespace Map_Editor_HoD.TilesModels
{
    public abstract class Tile : Interfaz.Models.Tiles.Tile
    {
        //private Furniture furniture = null;
        private Area area = new Area(new List<AreaDefiner>() {
            new AreaDefiner(),
            new AreaDefiner(),
            new AreaDefiner(),
            new AreaDefiner(),
        });

        public virtual new Area Area { get => area; set => area = value; }

        public virtual Furniture Furniture
        {
            get => (Furniture)base.Furniture;
            set
            {
                base.Furniture = value;
                if (base.Furniture != null)
                {
                    base.Furniture.Position = this.Position;
                }
            }
        }

        public override System.Numerics.Vector3 Position
        {
            get => base.Position;
            set
            {
                base.Position = value;
                if (entity != null)
                {
                    entity.Transform.Position = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3NumericToStride(value);
                }
            }
        }

        public override string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                if (entity != null)
                {
                    entity.Name = value;
                }

            }
        }

        private Entity entity = null;
        public Entity Entity
        {
            get
            {
                if (entity == null)
                {
                    if (string.IsNullOrWhiteSpace(base.Name))
                    {
                        return null;
                    }
                    SceneInstance sceneInstance = WorldController.game.SceneSystem.SceneInstance;
                    this.entity = sceneInstance.RootScene.Entities.Where(c => c.Name == base.Name).FirstOrDefault();
                    if (this.entity != null)
                    {
                        this.Position = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3StrideToNumeric(this.entity.Transform.Position);
                    }
                }
                return entity;
            }
            set => entity = value;
        }


        public Tile()
        {

        }

        public Tile(string name = "", Vector3 position = new(), Vector3 inworldpos = new()) : base(name, position, inworldpos)
        {
            Name = name;
            this.entity = null;
            base.Position = position;
            InWorldPos = inworldpos;
        }

        #region Auxiliares
        //public virtual void GetEntityByName(Game game, string name)
        //{
        //    Get the scene instance
        //    var sceneInstance = new Entity().Sc game.SceneSystem.SceneInstance;

        //    Search for the entity by name

        //   var entity = sceneInstance.RootScene.Entities.Find(name);

        //    Use the entity reference as needed
        //    if (entity != null)
        //    {
        //        Do something with the entity
        //    }
        //}

        public virtual new string ToJson()
        {
            try
            {
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new TileConverter(),
                        //new EntityConverter(),
                    }
                };

                string strResult = JsonSerializer.Serialize(this, serializeOptions);
                return strResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Puppet) String ToJson(): " + ex.Message);
                return string.Empty;
            }
        }

        public virtual new Tile FromJson(string Text)
        {
            string txt = Text;
            try
            {
                //string furniture = "";
                //string TileRemanent = txt;
                //string furnitureBase = "";
                //txt = UtilityAssistant.CleanJSON(txt.Replace("\u002B", "+"));

                //Obtiene el objeto furniture, y funciona para extraerlo
                //if (!txt.Contains("Furniture"))
                //{
                //    furniture = txt.Substring(txt.IndexOf("\"Furniture\":\""));
                //    txt = txt.Replace(furniture, "|°°|");
                //    txt = txt.Replace(",|°°|", "}");
                //}

                /*JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new TileConverter(),
                        new EntityConverter(),
                    }
                };*/

                //Tile strResult = JsonSerializer.Deserialize<Tile>(txt, serializeOptions);

                //if (txt.Contains("Furniture"))
                //{
                //    furniture = txt.Substring(txt.IndexOf("\"Furniture\":\""));
                //    furnitureBase = furniture.Replace("}\"}", "}");
                //    furniture = furniture.Substring(13);
                //    string strTemp = furniture.Replace("}\"}", "}");
                //    strTemp = strTemp.Replace("u003E", ">").Replace("u003C", "<");
                //    strTemp = strTemp.Replace("name", "\\\"name\\\"");
                //    strTemp = strTemp.Replace("\"\"name\"\"", "\\\"name\\\"");
                //    strTemp = strTemp.Replace("position", "\\\"position\\\"");
                //    strTemp = strTemp.Replace("\"\"position\"\"", "\\\"position\\\"");
                //    strTemp = strTemp.Replace("rotation", "\\\"rotation\\\"");
                //    strTemp = strTemp.Replace("\"\"rotation\"\"", "\\\"rotation\\\"");
                //    strTemp = strTemp.Replace("\\\"\"", "\\\"");
                //    strTemp = strTemp.Replace("\"\\\"", "\\\"");
                //    strTemp = strTemp.Replace("\\\"name\\\":\"", "\\\"name\\\":\\\"");
                //    strTemp = strTemp.Replace("\", \\\"position", "\\\", \\\"position");
                //    strTemp = strTemp.Replace("version=\"1.0\" encoding=\"utf-16\"", "version=\\\"1.0\\\" encoding=\\\"utf-16\\\"");
                //    furniture = strTemp.Replace("</Quaternion>}}", "</Quaternion>}\"}");
                //}

                //TileRemanent = TileRemanent.Replace(furnitureBase, "").Replace(",\"}", "}");

                //string clase = UtilityAssistant.ExtractValue(TileRemanent, "Class").Replace("\"", "");
                //string clase = UtilityAssistant.ExtractValue(txt, "Class").Replace("\"", "");

                //Type typ = Tile.TypesOfTiles().Where(c => c.Name == clase).FirstOrDefault();
                //if (typ == null)
                //{
                //    typ = Tile.TypesOfTiles().Where(c => c.FullName == clase).FirstOrDefault();

                //}

                //Tile tilObj = new Grass();
                //if (typ != null)
                //{
                //    object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                //                                                      //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list
                //    tilObj = ((Tile)obtOfType);
                //}

                JsonSerializerOptions serializeOpt = new JsonSerializerOptions
                {
                    Converters =
                        {
                            new TileConverter()
                        }
                };

                Tile tilObj = JsonSerializer.Deserialize<Tile>(txt, serializeOpt);

                /*string nombre = UtilityAssistant.ExtractValue(TileRemanent, "Name").Replace("\"", "");
                string posString = UtilityAssistant.ExtractValue(TileRemanent, "Pos").Replace("\"", "");
                string inWorldposString = UtilityAssistant.ExtractValue(TileRemanent, "InWorldPos").Replace("\"", "");

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    tilObj.Name = nombre;
                    this.Name = nombre;
                }

                if (!string.IsNullOrWhiteSpace(posString))
                {
                    tilObj.Position = Vector3Converter.Converter(posString);
                    this.Position = tilObj.Position;
                }

                if (!string.IsNullOrWhiteSpace(inWorldposString))
                {
                    tilObj.InWorldPos = Vector3Converter.Converter(inWorldposString);
                    this.InWorldPos = tilObj.InWorldPos;
                }

                if (!string.IsNullOrWhiteSpace(furniture))
                {
                    tilObj.Furniture = Furniture.CreateFromJson(furniture);
                    this.Furniture = tilObj.Furniture;
                }*/

                return tilObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Tile) FromJson: " + ex.Message + " Text: " + txt);
                return null;
            }
        }

        public static new Tile CreateFromJson(string json)
        {
            try
            {
                string cleanJson = json;
                /*if (!UtilityAssistant.IsValidJson(json))
                {
                    cleanJson = UtilityAssistant.CleanJSON(json);
                }*/
                //TODO: YOU NEED A NEW METHOD TO EXTRACT THE CLASS HERE
                //ALSO: ALL INTERFAZ CLASSES WILL BECOME "SAVER CLASES" CHECK THE FURNITURE INSIDE THE TILES EXAMPLE
                //TO REPLICATE IN THE OTHERS
                string clase = UtilityAssistant.ExtractValues(cleanJson, "Class").Replace("\"", "");

                if (clase.Contains(","))
                {
                    clase = clase.Substring(1, clase.IndexOf(",") - 1);
                }

                Type typ = Tile.TypesOfTiles().Where(c => c.Name == clase).FirstOrDefault();
                if (typ == null)
                {
                    typ = Tile.TypesOfTiles().Where(c => c.FullName == clase).FirstOrDefault();
                }

                object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                                                                  //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list

                Tile prgObj = ((Tile)obtOfType);
                return prgObj.FromJson(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Tile) CreateFromJson(): " + ex.Message);
                return null;
            }
        }

        public static new List<Type> TypesOfTiles()
        {
            List<Type> myTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Tile)) && !type.IsAbstract).ToList();
            return myTypes;
        }

        public virtual new void InstanceTile(string name = "", Vector3 position = default(Vector3), Vector3 inworldpos = default(Vector3))
        {
            try
            {
                Vector3 Pos = Vector3.Zero;
                if (position != default(Vector3))
                {
                    Pos = position;
                }

                if (inworldpos != default(Vector3))
                {
                    InWorldPos = inworldpos;
                }

                if (!string.IsNullOrEmpty(name))
                {
                    this.Name = name;
                }

                SpriteSheet spritesheet = null;
                string nameSprite = string.Empty;
                foreach (SpriteSheet spSht in Controller.controller.l_Tileset)
                {
                    foreach (Sprite sprite in spSht.Sprites)
                    {
                        if (sprite.Name == this.GetType().Name)
                        {
                            spritesheet = spSht;
                            nameSprite = sprite.Name;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(nameSprite))
                {
                    if (entity == null)
                    {
                        this.entity = new Entity(this.Name);
                        Controller.controller.Entity.Scene.Entities.Add(this.entity);
                        //SceneInstance sceneInstance = WorldController.game.SceneSystem.SceneInstance;
                        //this.entity = sceneInstance.RootScene.Entities.Where(c => c.Name == base.Name).FirstOrDefault();
                        entity.GetOrCreate<SpriteComponent>().SpriteProvider = SpriteFromSheet.Create(spritesheet, nameSprite);
                    }

                    if (Pos != default(Vector3))
                    {
                        this.Position = Pos;
                    }

                    // Get the size of the sprite
                    spriteSize = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector2StrideToNumeric(entity.GetOrCreate<SpriteComponent>().CurrentSprite.Size);

                    // Calculate the corners of the sprite
                    Vector3 topLeft = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3StrideToNumeric(entity.Transform.WorldMatrix.TranslationVector) + new Vector3(-spriteSize.X / 2, spriteSize.Y / 2, 0);
                    Vector3 topRight = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3StrideToNumeric(entity.Transform.WorldMatrix.TranslationVector) + new Vector3(spriteSize.X / 2, spriteSize.Y / 2, 0);
                    Vector3 bottomLeft = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3StrideToNumeric(entity.Transform.WorldMatrix.TranslationVector) + new Vector3(-spriteSize.X / 2, -spriteSize.Y / 2, 0);
                    Vector3 bottomRight = Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertVector3StrideToNumeric(entity.Transform.WorldMatrix.TranslationVector) + new Vector3(spriteSize.X / 2, -spriteSize.Y / 2, 0);
                    this.Area.L_AreaDefiners[0].Point = new Pares<string, SerializedVector3>() { Item1 = "NW", Item2 = new SerializedVector3(topLeft) };
                    this.Area.L_AreaDefiners[1].Point = new Pares<string, SerializedVector3>() { Item1 = "NE", Item2 = new SerializedVector3(topRight) };
                    this.Area.L_AreaDefiners[2].Point = new Pares<string, SerializedVector3>() { Item1 = "SW", Item2 = new SerializedVector3(bottomLeft) };
                    this.Area.L_AreaDefiners[3].Point = new Pares<string, SerializedVector3>() { Item1 = "SE", Item2 = new SerializedVector3(bottomRight) };

                    //SceneInstance sceneInstance = WorldController.game.SceneSystem.SceneInstance;
                    //this.entity = sceneInstance.RootScene.Entities.Where(c => c.Name == base.Name).FirstOrDefault();

                    //Correct system rotation
                    //Entity.Transform.Rotation *= Quaternion.RotationX(Convert.ToSingle(Map_Editor_HoD.Code.Assistants.UtilityAssistant.ConvertDegreesToRadiants(90)));

                    //More precise rotation
                    entity.Transform.Rotation *= Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertSystemNumericsToStrideQuaternion(System.Numerics.Quaternion.CreateFromAxisAngle(Vector3.UnitX, MathF.PI / 2));

                    //Entity.Transform.Position = Code.Assistants.UtilityAssistant.ConvertVector3NumericToStride(Pos);
                    return;
                }
                Console.WriteLine("Error (Map_Editor_HoD.Models.TilesModels.Tile) InstanceTile: SPRITE NO ENCONTRADO PARA CLASE " + this.GetType().FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Map_Editor_HoD.Models.TilesModels.Tile) InstanceTile: " + ex.Message);
            }
        }

        public virtual void InstanceEditorReqMechanics()
        {
            try
            {
                BoxColliderShape colShape = new BoxColliderShape(false, new Stride.Core.Mathematics.Vector3(0.8f, 0.1f, 0.8f));
                StaticColliderComponent sComp = new StaticColliderComponent();
                sComp.CollisionGroup = Stride.Physics.CollisionFilterGroups.CustomFilter1;
                sComp.ColliderShape = colShape;

                this.Entity.Add(sComp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Map_Editor_HoD.Models.TilesModels.Tile) InstanceEditorReqMechanics: " + ex.Message);
            }
        }

        public virtual Tile ChangeType(string nameOfSelectedType, string nameInWorld)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nameOfSelectedType))
                {
                    Console.WriteLine("Tile ChangeType: " + nameOfSelectedType);
                    return null;
                }

                Type typ = Tile.TypesOfTiles().Where(c => c.Name == nameOfSelectedType).FirstOrDefault();
                if (typ == null)
                {
                    typ = Tile.TypesOfTiles().Where(c => c.FullName == nameOfSelectedType).FirstOrDefault();
                }

                object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                                                                  //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list

                Tile prgObj = ((Tile)obtOfType);
                Tile rnTile = this;

                if (rnTile != null)
                {
                    prgObj.Position = rnTile.Position;
                    prgObj.Area = rnTile.Area;
                    prgObj.Name = rnTile.Name;
                    prgObj.InWorldPos = rnTile.InWorldPos;
                    prgObj.Furniture = rnTile.Furniture;

                    if (rnTile.entity != null)
                    {
                        prgObj.entity = rnTile.entity;
                        prgObj.entity.GetOrCreate<SpriteComponent>().SpriteProvider = SpriteFromSheet.Create(Controller.controller.SelectedSpriteSheet, nameOfSelectedType);

                    }
                }

                bool isDone = false;
                do
                {
                    isDone = WorldController.TestWorld.dic_worldTiles.TryUpdate(nameInWorld, prgObj, rnTile);
                    //isDone = WorldController.TestWorld.dic_worldTiles.TryAdd(nameInWorld, prgObj);
                }
                while (!isDone);
                return prgObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Map_Editor_HoD.Models.TilesModels.Tile) ChangeType(string): " + ex.Message);
                return null;
            }
        }
        #endregion

    }

    public class TileConverter : System.Text.Json.Serialization.JsonConverter<Tile>
    {
        public override Tile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string strJson = string.Empty;
            try
            {
                //TODO: Corregir, testear y terminar
                JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                strJson = jsonDoc.RootElement.GetRawText();
                //strJson = reader.GetString();

                string clase = UtilityAssistant.CleanJSON(strJson);
                clase = UtilityAssistant.ExtractValue(clase, "Class").Replace("\"", "");

                Type typ = Tile.TypesOfTiles().Where(c => c.Name == clase).FirstOrDefault();
                if (typ == null)
                {
                    typ = Tile.TypesOfTiles().Where(c => c.FullName == clase).FirstOrDefault();
                }

                object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                                                                  //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list

                Tile prgObj = ((Tile)obtOfType);

                string pst = UtilityAssistant.ExtractValue(strJson, "Pos");
                prgObj.Position = UtilityAssistant.Vector3Deserializer(pst);
                pst = UtilityAssistant.ExtractValue(strJson, "InWorldPos");
                prgObj.InWorldPos = UtilityAssistant.Vector3Deserializer(pst);
                prgObj.Name = UtilityAssistant.ExtractValue(strJson, "Name");
                pst = UtilityAssistant.ExtractValue(strJson, "Furniture");
                if (!string.IsNullOrWhiteSpace(pst) && pst != "}")
                {
                    pst = pst.Replace("}", "");
                    prgObj.Furniture = Furniture.CreateFromJson(UtilityAssistant.Base64Decode(pst));
                }

                pst = UtilityAssistant.ExtractValue(strJson, "Entity");
                JsonSerializerOptions serializeOpt = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new EntityConverter()
                    }
                };

                prgObj.Entity = JsonSerializer.Deserialize<Entity>(UtilityAssistant.Base64Decode(pst), serializeOpt);

                SpriteSheet spritesheet = null;
                string nameSprite = string.Empty;
                foreach (SpriteSheet spSht in Controller.controller.l_Tileset)
                {
                    foreach (Sprite sprite in spSht.Sprites)
                    {
                        if (sprite.Name == prgObj.GetType().Name)
                        {
                            spritesheet = spSht;
                            nameSprite = sprite.Name;
                        }
                    }
                }

                prgObj.Entity.GetOrCreate<SpriteComponent>().SpriteProvider = SpriteFromSheet.Create(spritesheet, nameSprite);

                //prgObj.Entity.Transform.Rotation *= Map_Editor_HoD.Assistants.StrideUtilityAssistant.ConvertSystemNumericsToStrideQuaternion(System.Numerics.Quaternion.CreateFromAxisAngle(System.Numerics.Vector3.UnitX, MathF.PI / 2));
                //prgObj.Entity.Transform.Rotation *= Quaternion.RotationYawPitchRoll(90, 0, 0);
                //prgObj.Entity.Transform.Rotation *= Quaternion.RotationX(Convert.ToSingle(Map_Editor_HoD.Code.Assistants.UtilityAssistant.ConvertDegreesToRadiants(90)));

                prgObj.InstanceEditorReqMechanics();
                Controller.controller.Entity.Scene.Entities.Add(prgObj.Entity);

                return prgObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: (TileConverter) Read(): {0} Message: {1}", strJson, ex.Message);
                return default;
            }
        }

        public override void Write(Utf8JsonWriter writer, Tile tle, JsonSerializerOptions options)
        {
            try
            {
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new Vector3Converter()
                        ,new NullConverter()
                    },
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true,
                    IgnoreNullValues = true
                };


                //Para deserealizar los vector3 serializados: UtilityAssistant.Vector3Deserializer(tle);

                //TODO: Corregir, testear y terminar
                string Name = string.IsNullOrWhiteSpace(tle.Name) ? "null" : tle.Name;
                string Position = System.Text.Json.JsonSerializer.Serialize(tle.Position, serializeOptions);
                string InWorldPos = System.Text.Json.JsonSerializer.Serialize(tle.InWorldPos, serializeOptions);
                string Furniture = "\"\"";
                string entity = "\"\"";
                if (tle.Furniture != null)
                {
                    Furniture = UtilityAssistant.Base64Encode(tle.Furniture.ToJson());
                }

                if (tle.Entity != null)
                {
                    JsonSerializerOptions serializeOpt = new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new EntityConverter()
                        }
                    };

                    entity = UtilityAssistant.Base64Encode(JsonSerializer.Serialize(tle.Entity, serializeOpt));
                }

                string Class = tle.GetType().Name;

                char[] a = { '"' };

                string wr = string.Concat("{ ", new string(a), "Name", new string(a), ":", new string(a), Name, new string(a),
                    ", ", new string(a), "Class", new string(a), ":", new string(a), Class, new string(a),
                    ", ", new string(a), "Pos", new string(a), ":", Position, 
                    ", ", new string(a), "InWorldPos", new string(a), ":", InWorldPos,
                    ", ", new string(a), "Entity", new string(a), ":", new string(a), entity, new string(a),
                    ", ", new string(a), "Furniture", new string(a), ":", new string(a), Furniture, new string(a),
                    "}");

                string resultJson = Regex.Replace(wr, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                //string resultJson = "{Id:" + Id + ", LN:" + LauncherName + ", Type:" + Type + ", OrPos:" + LauncherPos + ", WPos:" + WeaponPos + ", Mdf:" + Moddif + "}";
                writer.WriteStringValue(resultJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: (TileConverter) Write(): " + ex.Message);
            }
        }
    }

}