using Map_Editor_HoD.Assistants;
using Map_Editor_HoD.Controllers;
using Stride.Engine;
using System.Collections.Generic;

namespace Map_Editor_HoD.FurnitureModels
{
    public class Muro_de_Tierra : Map_Editor_HoD.FurnitureModels.Furniture
    {
        public Muro_de_Tierra()
        {
        }

        public Muro_de_Tierra(string TypeOfPuppetName, string name = "")
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

        public Muro_de_Tierra(string name = "")
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
    }

}
