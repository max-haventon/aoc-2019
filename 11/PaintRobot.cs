using System;
using System.Collections.Generic;
using Common;

namespace Day11
{
    public class PaintRobot {
        private IntCodeComputer icc;
        
        private long[] iccInput;
        public int x, y;
        public Direction direction;
        public List<PaintedPoint> paintedPoints = new List<PaintedPoint>() {};
        
        public PaintRobot(IntCodeComputer icc) {
            this.icc = icc;
            iccInput = new long[] {};
            direction = Direction.Up;

            x = 0;
            y = 0;
        }

        public PaintRobot(IntCodeComputer icc, PixelColor originColor) {
            this.icc = icc;
            iccInput = new long[] {};
            direction = Direction.Up;
            
            x = 0;
            y = 0;
            paintedPoints.Add(new PaintedPoint(0, 0, originColor));
        }

        public List<PaintedPoint> PaintThatHull() {
            int pixelsPainted = 0;
            while (icc.mode == "RUNNING") {
                var color = GetColor(x, y);

                try {
                    (PixelColor colorToPaint, DirectionChange directionChange) = WhatToDo(color);

                    LogPaintResult(colorToPaint);
                
                    UpdatePositionAndDirection(directionChange);

                    pixelsPainted++;

                    if (pixelsPainted > 100000) {
                        Console.WriteLine($"I have now painted more than 1000 pixels, that surely must be enough? {paintedPoints.Count} distinct points.");
                        throw new Exception("TooMuchPaintException");
                    }
                } catch (Exception) {
                    return paintedPoints;
                }
            }
            
            return paintedPoints;
        }

        public (PixelColor colorToPaint, DirectionChange directionChange) WhatToDo(PixelColor currentColor) {
            List<long> output = new List<long>();

            long[] oldIccInput = iccInput;
            iccInput = new long[oldIccInput.Length + 1];
            for (int i=0; i<oldIccInput.Length; i++) {
                iccInput[i] = oldIccInput[i];
            }
            iccInput[iccInput.Length - 1] = (long)(int)currentColor;

            int stepsTaken = 0;
            while (output.Count < 2) {
                if (icc.mode != "RUNNING")
                    throw new Exception();

                icc.step(iccInput, output);
                stepsTaken++;

                if(stepsTaken > 100) {
                    Console.WriteLine($"More than 100 steps taken trying to get output from the icc. Have {output.Count} so far.");
                    throw new Exception("IccHungException");
                }
            }

            return ((PixelColor)output[0], (DirectionChange)output[1]);
        }

        private PixelColor GetColor(int x, int y) {
            foreach (PaintedPoint paintedPoint in paintedPoints) {
                if (paintedPoint.x == x && paintedPoint.y == y) {
                    Console.WriteLine($"Found color {paintedPoint.color} at {x}, {y}");
                    return paintedPoint.color;
                }
            }

            return PixelColor.Black;
        }

        private void LogPaintResult(PixelColor colorToPaint)
        {
            foreach (PaintedPoint paintedPoint in paintedPoints) {
                if (paintedPoint.x == x && paintedPoint.y == y) {
                    paintedPoint.color = colorToPaint;
                    return;
                }
            }
            
            Console.WriteLine($"Added {x},{y}");

            paintedPoints.Add(new PaintedPoint(x, y, colorToPaint));
        }

        public void UpdatePositionAndDirection(DirectionChange directionChange)
        {
            UpdateDirection(directionChange);
            UpdatePosition();
        }

        public void UpdateDirection(DirectionChange directionChange)
        {
            switch (directionChange) {
                case DirectionChange.TurnLeft:
                    if (direction == Direction.Up) {
                        direction = Direction.Left;
                    } else {
                        direction -= 1;
                    }
                    break;
                case DirectionChange.TurnRight:
                    if (direction == Direction.Left) {
                        direction = Direction.Up;
                    } else {
                        direction += 1;
                    }
                    break;
                default:
                    throw new Exception("Unsupported direction change");
            }
        }

        public void UpdatePosition()
        {
            switch (direction) {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                default:
                    throw new Exception("Unsupported direction");
            }
        }
    }
}
