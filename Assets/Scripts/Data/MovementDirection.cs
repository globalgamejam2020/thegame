using System;
using UnityEngine;

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
        
        public static Vector3 Up(this MovementDirection self) {
            var up = Vector3.zero;

            if (self.Matches(MovementDirection.NORTH)) up.y = 1;
            else if (self.Matches(MovementDirection.SOUTH)) up.y = -1;

            if (self.Matches(MovementDirection.EAST)) up.x = 1;
            else if (self.Matches(MovementDirection.WEST)) up.x = -1;

            return up;
        }
    }
}