using System;

namespace Global {
    public enum ObjectType {
        TREE,
        WATER,
        BUILDING,
        PAVEMENT,
        GRASS
    }

    public static class ObjectTypeImplementation {
        public static bool AllowsVision(this ObjectType self) {
            switch (self) {
                case ObjectType.TREE:
                case ObjectType.BUILDING:
                    return false;
                default:
                    return true;
            }
        }

        public static bool AllowsMovement(this ObjectType self) {
            switch (self) {
                case ObjectType.BUILDING:
                case ObjectType.WATER:
                    return false;
                default:
                    return true;
            }
        }
    }
}