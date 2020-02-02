using System;

namespace Data {
    [Flags]
    public enum MovementDirection : byte {
        NORTH = 1,
        EAST = 2,
        SOUTH = 4,
        WEST = 8
    }

    public static class MovementDirectionImplementation {
        public static bool Matches(this MovementDirection self, MovementDirection other) {
            return ((byte) self & (byte) other) != 0;
        }
    }
}