using System;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 0;                       // Можно задать полям значения по умолчанию
        public int wheat = 0;
        public int carrot = 0;
        public int radish = 0;
        public int potato = 0;

        public Vector3 playerPosition = new Vector3(0, 3, 0);
        public List<Isle> Isles = new List<Isle>();

        // Ваши сохранения

        // ...

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        // Пока выявленное ограничение - это расширение массива


    }
}
