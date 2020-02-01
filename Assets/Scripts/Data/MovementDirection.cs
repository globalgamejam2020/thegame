using System;

namespace Data {
    [Flags]
    public enum MovementDirection : byte {
        IDLE = 0,
        NORTH = 1,
        EAST = 2,
        SOUTH = 4,
        WEST = 8
    }

    [Flags]
    public enum MovementStyle : byte
    {
        IDLE = 1,
        STRAIGHT = 2,
        TURNL = 4,
        TURNR = 8
    }

    public static class MovementDirectionImplementation {
        public static bool Matches(this MovementDirection self, MovementDirection other) {
            return ((byte) self & (byte) other) != 0;
        }
    }
}