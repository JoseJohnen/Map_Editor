using Interfaz.Utilities;
using Map_Editor_HoD.Assistants;
using Map_Editor_HoD.Controllers;
using Stride.Core.Extensions;
using Stride.Engine;
using Stride.Graphics;
using Stride.Rendering;
using Stride.Rendering.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using StrideUtilityAssistant = Map_Editor_HoD.Assistants.StrideUtilityAssistant;

namespace Map_Editor_HoD.FurnitureModels
{
    public abstract class Furniture : Interfaz.Models.Furnitures.Furniture
    {
        public virtual new Area Area { get => area; set => area = value; }
        private Prefab prefab = null;

        private Area area = new Area(new List<AreaDefiner>() {
            new AreaDefiner(),
            new AreaDefiner(),
            new AreaDefiner(),
            new AreaDefiner(),
        });

        public override Vector3 Position
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

        public override Vector3 Rotation
        {
            get => base.Rotation;
            set
            {
                base.Rotation = value;
                if (entity != null)
                {
                    Vector3 rotVec3 = value;
                    entity.Transform.Rotation.X = rotVec3.X;
                    entity.Transform.Rotation.Y = rotVec3.Y;
                    entity.Transform.Rotation.Z = rotVec3.Z;
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

        [JsonConverter(typeof(EntityConverter))]
        private Entity entity = null;
        public Entity Entity
        {
            get
            {
                if (entity == null)
                {
                    //No anti ruptura, sino que un generador a partir de la clase seleccionada
                    //if (string.IsNullOrWhiteSpace(base.Name))
                    //{
                    //    return null;
                    //}
                    //SceneInstance sceneInstance = WorldController.game.SceneSystem.SceneInstance;
                    //this.entity = sceneInstance.RootScene.Entities.Where(c => c.Name == base.Name).FirstOrDefault();

                    this.entity = Controller.controller.GetPrefab(this.GetType().Name).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(base.Name))
                    {
                        this.entity.Name = base.Name;
                    }

                    WorldController.game.SceneSystem.SceneInstance.RootScene.Entities.Add(this.entity);
                }
                return entity;
            }
            set => entity = value;
        }

        public Furniture(string TypeOfPuppetName, string name = "")
        {
            this.Name = name;

            List<Entity> prefab = Controller.controller.GetPrefab(TypeOfPuppetName);
            this.Entity = prefab[0];
            this.Entity.Name = name + "_" + TypeOfPuppetName;
            this.Entity.Transform.Position = StrideUtilityAssistant.ConvertVector3NumericToStride(this.Position);

            this.Entity.Transform.Rotation.X = this.Rotation.X;
            this.Entity.Transform.Rotation.Y = this.Rotation.Y;
            this.Entity.Transform.Rotation.Z = this.Rotation.Z;

            WorldController.game.SceneSystem.SceneInstance.RootScene.Entities.Add(this.Entity);
        }

        public Furniture(string name = "")
        {
            this.Name = name;

            List<Entity> prefab = Controller.controller.GetPrefab(this.GetType().Name);
            this.Entity = prefab[0];
            this.Entity.Name = name + "_" + this.GetType().Name;
            this.Entity.Transform.Position = StrideUtilityAssistant.ConvertVector3NumericToStride(this.Position);

            this.Entity.Transform.Rotation.X = this.Rotation.X;
            this.Entity.Transform.Rotation.Y = this.Rotation.Y;
            this.Entity.Transform.Rotation.Z = this.Rotation.Z;

            WorldController.game.SceneSystem.SceneInstance.RootScene.Entities.Add(this.Entity);
        }

        public Furniture()
        {

        }

        public override bool Equals(object obj)
        {
            // If the passed object is null, return False
            if (obj == null)
            {
                return false;
            }
            // If the passed object is not Customer Type, return False
            if (!(obj is Furniture))
            {
                return false;
            }
            return (this.Name == ((Furniture)obj).Name)
                && (this.Position == ((Furniture)obj).Position)
                && (this.Rotation == ((Furniture)obj).Rotation)
                && (this.entity.Name == ((Furniture)obj).entity.Name);
        }

        public virtual bool RemoveFurnitureSafeFromScene()
        {
            try
            {
                WorldController.game.SceneSystem.SceneInstance.RootScene.Entities.Remove(this.entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("(Furniture) RemoveFurnitureSafe() Error: " + ex.Message);
                return false;
            }
        }

        public static new List<Type> TypesOfFurniture()
        {
            List<Type> myTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Furniture)) && !type.IsAbstract).ToList();
            return myTypes;
        }

        #region JSON
        public virtual string ToJson()
        {
            try
            {
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new FurnitureConverter(),
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

        public virtual Furniture FromJson(string Text)
        {
            string txt = Text;
            try
            {
                txt = Interfaz.Utilities.UtilityAssistant.CleanJSON(txt.Replace("\u002B", "+"));

                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new FurnitureConverter(),
                        //new EntityConverter(),
                        new NullConverter(),
                    }
                };



                //txt = Regex.Replace(txt, @"\s+", "|-");
                //txt = txt.Replace("|-", " ");
                /*RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                txt = regex.Replace(txt, " ");*/

                /*StringBuilder sb = new StringBuilder(txt.Length);
                bool spaceAdded = false;
                for (int i = 0; i < txt.Length; i++)
                {
                    char c = txt[i];
                    if (c == ' ')
                    {
                        if (!spaceAdded)
                        {
                            sb.Append(' ');
                            spaceAdded = true;
                        }
                    }
                    else
                    {
                        sb.Append(c);
                        spaceAdded = false;
                    }
                }
                txt = sb.ToString();*/

                //txt = txt.Replace("version=1.0", "version=u00221.0u0022");
                //txt = txt.Replace("encoding=utf-16", "encoding=u0022utf-16u0022");
                //txt = txt.Replace("version=\"1.0\"", "version=u00221.0u0022");
                //txt = txt.Replace("encoding=\"utf-16\"", "encoding=u0022utf-16u0022");
                //txt = txt.Replace("Interfaz", "u0022Interfazu0022").Replace("\"\"Interfaz\"\"", "u0022Interfazu0022");
                //txt = txt.Replace("Map_Editor_HoD", "u0022Map_Editor_HoDu0022").Replace("\"\"Map_Editor_HoD\"\"", "u0022Map_Editor_HoDu0022");
                //txt = txt.Replace(">rn", ">");

                Furniture strResult = JsonSerializer.Deserialize<Furniture>(txt, serializeOptions);

                //TODO: VER QUE EL OBJETO AL HACER TO JSON SALVE EL NOMBRE DE LA CLASE TAMBIÉN
                //TODO2: RECUERDA QUE DEBES EXTRAER EL OBJETO u0022

                if (strResult != null)
                {
                    this.Name = strResult.Name;
                    this.Position = strResult.Position;
                    this.Rotation = strResult.Rotation;
                    this.prefab = strResult.prefab;
                    this.Entity = strResult.Entity;
                    this.Area = strResult.Area;
                }
                return strResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Tile) FromJson: " + ex.Message + " Text: " + txt);
                return null;
            }
        }

        public static Furniture CreateFromJson(string json)
        {
            try
            {
                string clase = Interfaz.Utilities.UtilityAssistant.CleanJSON(json);
                clase = Interfaz.Utilities.UtilityAssistant.PrepareJSON(clase);
                clase = StrideUtilityAssistant.ExtractValue(clase, "Class").Replace("\"", "");

                Type typ = Furniture.TypesOfFurniture().Where(c => c.Name == clase).FirstOrDefault();
                if (typ == null)
                {
                    typ = Furniture.TypesOfFurniture().Where(c => c.FullName == clase).FirstOrDefault();
                }

                object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                                                                  //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list

                Furniture prgObj = ((Furniture)obtOfType);
                return prgObj.FromJson(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error (Tile) CreateFromJson(): " + ex.Message);
                return null;
            }
        }
        #endregion
    }

    public class FurnitureConverter : System.Text.Json.Serialization.JsonConverter<Furniture>
    {
        public override Furniture Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string[] strJsonArray = new string[1];
            string[] strStrArr = new string[1];
            //string[] strStrArr2 = new string[1];
            //string[] strStrArr3 = new string[1];
            string readerReceiver = string.Empty;
            try
            {
                //TODO: Corregir, testear y terminar
                //string tempString = reader.GetString();
                JsonDocument jsonDoc = JsonDocument.ParseValue(ref reader);
                string tempString = jsonDoc.RootElement.GetRawText();

                string clase = UtilityAssistant.ExtractValue(tempString, "Class").Replace("\"", "");

                string nombre = UtilityAssistant.ExtractValue(tempString, "Name").Replace("\"", "").Replace("}", "");
                string area = UtilityAssistant.ExtractValue(tempString, "Area").Replace("\"", "");
                string entity = UtilityAssistant.ExtractValue(tempString, "Entity").Replace("\"", "");
                string posString = UtilityAssistant.ExtractValue(tempString, "Position").Replace("\"", "");
                string rotationString = UtilityAssistant.ExtractValue(tempString, "Rotation").Replace("\"", "");

                Type typ = Furniture.TypesOfFurniture().Where(c => c.Name == clase).FirstOrDefault();
                if (typ == null)
                {
                    typ = Furniture.TypesOfFurniture().Where(c => c.FullName == clase).FirstOrDefault();
                }

                Furniture furnObj = new Muro_de_Tierra();
                if (typ != null)
                {
                    object obtOfType = Activator.CreateInstance(typ); //Requires parameterless constructor.
                                                                      //TODO: System to determine the type of enemy to make the object, prepare stats and then add it to the list
                    furnObj = ((Furniture)obtOfType);
                }

                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    furnObj.Name = nombre;
                }

                if (!string.IsNullOrWhiteSpace(area))
                {
                    furnObj.Area = Area.CreateFromJson(UtilityAssistant.Base64Decode(area));
                }

                if (!string.IsNullOrWhiteSpace(posString))
                {
                    furnObj.Position = StrideUtilityAssistant.ConvertVector3StrideToNumeric(StrideUtilityAssistant.XmlToClass<SerializedVector3>(UtilityAssistant.Base64Decode(posString)).ConvertToVector3());
                }

                if (!string.IsNullOrWhiteSpace(rotationString))
                {
                    furnObj.Rotation = StrideUtilityAssistant.ConvertVector3StrideToNumeric(StrideUtilityAssistant.XmlToClass<SerializedVector3>(UtilityAssistant.Base64Decode(rotationString)).ConvertToVector3());
                }

                if (!string.IsNullOrWhiteSpace(entity))
                {
                    JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                    {
                        Converters =
                        {
                            new EntityConverter()
                        }
                    };

                    furnObj.Entity = JsonSerializer.Deserialize<Entity>(UtilityAssistant.Base64Decode(entity), serializeOptions);
                }

                string nameSprite = string.Empty;
                foreach (KeyValuePair<string,Model> kvp in Controller.controller.dic_Models)
                {
                    if (kvp.Key.ToUpper().Contains(furnObj.GetType().Name.ToUpper()))
                    {
                        furnObj.Entity.GetOrCreate<ModelComponent>().Model = kvp.Value;
                    }
                }

                Controller.controller.Entity.Scene.Entities.Add(furnObj.Entity);

                return furnObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: (WorldConverter) Read(): {0} Message: {1}", strJsonArray[0], ex.Message);
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, Furniture frnObj, JsonSerializerOptions options)
        {
            try
            {
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new EntityConverter()
                    }
                };

                string strTemp = string.Empty;
                strTemp =
                    "{\"Class\":\"" + frnObj.GetType().Name + "\"," +
                    "\"Area\":\"" + UtilityAssistant.Base64Encode(frnObj.Area.ToJson()) + "\"," +
                    "\"Position\":\"" + UtilityAssistant.Base64Encode(new Interfaz.Utilities.SerializedVector3(frnObj.Position).ToXML()) + "\"," +
                    "\"Rotation\":\"" + UtilityAssistant.Base64Encode(new Interfaz.Utilities.SerializedVector3(frnObj.Rotation).ToXML()) + "\"," +
                "\"Entity\":\"" + UtilityAssistant.Base64Encode(JsonSerializer.Serialize(frnObj.Entity, serializeOptions)) + "\"," +
                "\"Name\":\"" + frnObj.Name + "\"}";

                writer.WriteStringValue(strTemp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: (WorldConverter) Write(): " + ex.Message);
            }
        }
    }

}