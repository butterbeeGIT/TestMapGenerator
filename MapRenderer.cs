

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

           
        }

        /// <summary>
        /// строит горизонтальный коридор при необходимости
        /// </summary>
        /// <param name="x1">начальная точка</param>
        /// <param name="y1"></param>
        /// <param name="x2">конечная точка</param>
        /// <param name="y2"></param>
        /// <param name="map">символьная карта</param>
        /// <returns>координата x до которой удалось построить коридор</returns>
        public static int AddSymbolicHorizontalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
        {

            while (x1 != x2)
            {
                int yOffset = y1; // Ограничиваем смещение, чтобы не выйти за границы карты.
                char originalSymb = map[yOffset, x1];
                int nextX = x1 < x2 ? x1 + 1 : x1-1;
                if (originalSymb == '#' && map[yOffset, nextX] == '#')
                {
                    if (y1 != y2) 
                    {
                        y1 = AddSymbolicVerticalCorridorOnMap(x1, y1, x2, y2, map);
                        break; 
                    }
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

        /// <summary>
        /// строит вертикальный коридор при необходимости
        /// </summary>
        /// <param name="x1">начальная точка</param>
        /// <param name="y1"></param>
        /// <param name="x2">конечная точка</param>
        /// <param name="y2"></param>
        /// <param name="map">символьная карта</param>
        /// <returns>координата y до которой удалось построить коридор</returns>

        public static int AddSymbolicVerticalCorridorOnMap(int x1, int y1, int x2, int y2, char[,] map)
        {
            while (y1 != y2)
            {
                int xOffset = x1; // Ограничиваем смещение.
                char originalSymb = map[y1, xOffset];
                int nextY = y1 < y2 ? y1+1 : y1-1;
                if (originalSymb == '#' && map[nextY, xOffset] == '#')
                {
                    if (x1 != x2) 
                    {
                        x1 = AddSymbolicHorizontalCorridorOnMap(x1, y1, x2, y2, map);
                        break;
                    }
                   
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
