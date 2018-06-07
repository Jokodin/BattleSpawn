using UnityEngine;

namespace ConstantData
{
    public class Constants
    {
        public static readonly string TEAM1 = "Team1";
        public static readonly string TEAM2 = "Team2";
        public static float ScrollSpeed { get { return 1000; } }
        public static int ScrollWidth { get { return 20; } }
        public static float RotateAmount { get { return 5; } }
        public static float RotateSpeed { get { return 500; } }
        public static float MinCameraHeight { get { return 20; } }
        public static float MaxCameraHeight { get { return 600; } }
        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
        public static Vector3 InvalidPosition { get { return invalidPosition; } }

        
    }

    public class Enums
    {
        public enum ResourceType { Gold, Power };
    }


}
