using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMapGenerator
{
    /// <summary>
    /// Отрисовывает карту и объекты карты
    /// </summary>
    public static class MapRenderer
    {
        /// <summary>
        /// добавляет на карту символически отрисованную комнату
        /// </summary>
        /// <param name="room"></param>
        /// <param name="map"></param>
        public static void AddSymbolicRoomOnMap(Room room, char[,] map)
        {
            for (int i = room.Y; i < room.Y + room.Height; i++)
            {
                for (int j = room.X; j < room.X + room.Width; j++)
                {
                    if (room.isWall(j, i))
                        map[i, j] = '#';
                    else
                        map[i, j] = '.';
                }
            }
        }

        /// <summary>
        /// добавляет на карту символически отрисованный коридор
        /// </summary>
        /// <param name="room"></param>
        /// <param name="map"></param>
        public static void AddSymbolicCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
        {     
            x1 = AddSymbolicHorizontalCorridorOnMap(x1, y1, x2, y2, map);
            AddSymbolicVerticalCorridorOnMap(x1, y1, x2, y2, map);

            //// Создаём горизонтальный коридор.
            //while (x1 != x2)
            //{
            //    int yOffset = y1; // Ограничиваем смещение, чтобы не выйти за границы карты.
            //    char originalSymb = map[yOffset, x1];
            //    int nextX = x1 < x2 ? 1 : -1;
            //    if (originalSymb == '#' && map[yOffset, nextX] == '#')
            //    {

            //    }
            //    map[yOffset, x1] = originalSymb switch
            //    {
            //        ' ' => '-',
            //        '#' => 'D',
            //        _ => originalSymb
            //    };
            //    x1 += x1 < x2 ? 1 : -1; // Двигаемся к целевой точке.
            //}

            //// Создаём вертикальный коридор.
            //while (y1 != y2)
            //{
            //    int xOffset = x1; // Ограничиваем смещение.
            //    char originalSymb = map[y1, xOffset];

            //    map[y1, xOffset] = originalSymb switch
            //    {
            //        ' ' => '|',
            //        '#' => 'D',
            //        _ => originalSymb
            //    };
            //    y1 += y1 < y2 ? 1 : -1; // Двигаемся к целевой точке.
            //}
        }


        public static int AddSymbolicHorizontalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
        {//System.StackOverflowException: "Exception_WasThrown"

            while (x1 != x2)
            {
                int yOffset = y1; // Ограничиваем смещение, чтобы не выйти за границы карты.
                char originalSymb = map[yOffset, x1];
                int nextX = x1 < x2 ? x1 + 1 : x1-1;
                if (originalSymb == '#' && map[yOffset, nextX] == '#')
                {
                    y1 = AddSymbolicVerticalCorridorOnMap(x1, y1, x2, y2, map);
                    originalSymb = map[yOffset, x1];
                }
                
                map[yOffset, x1] = originalSymb switch
                {
                    ' ' => '-',
                    '#' => 'D',
                    _ => originalSymb
                };
                x1 += x1 < x2 ? 1 : -1; // Двигаемся к целевой точке.
            }
            return x1;
        }


        public static int AddSymbolicVerticalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
        {
            while (y1 != y2)
            {
                int xOffset = x1; // Ограничиваем смещение.
                char originalSymb = map[y1, xOffset];
                int nextY = y1 < y2 ? y1+1 : y1-1;
                if (originalSymb == '#' && map[nextY, xOffset] == '#')
                {
                    x1 = AddSymbolicHorizontalCorridorOnMap(x1, y1, x2, y2, map);
                    originalSymb = map[y1, xOffset];
                }
                map[y1, xOffset] = originalSymb switch
                {
                    ' ' => '|',
                    '#' => 'D',
                    _ => originalSymb
                };
                y1 += y1 < y2 ? 1 : -1; // Двигаемся к целевой точке.
            }
            return y1;
        }



    }
}
