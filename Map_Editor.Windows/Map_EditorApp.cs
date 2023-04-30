using Stride.Engine;

namespace Map_Editor_HoD
{
    class Map_EditorApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
