using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using Statuses.API.Interfaces;
using Statuses.API.Enums;
using Statuses.API.Features;
using Statuses.API.Features.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Statuses.API
{
    public static class API
    {
        public const float RotationSpeed = 15;

        private static List<IStatus> RegisteredStatuses = new List<IStatus>();

        private static Dictionary<string, List<StatusInfo>> PlayerStatuses = new Dictionary<string, List<StatusInfo>>();

        public static void Register<T>() where T : IStatus, new()
        {
            RegisteredStatuses.Add(new T());
        }

        public static void Enable<T>(Player player) where T : IStatus, new()
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();
            }

            if (Contains<T>(player))
            {
                return;
            }

            var status = new T();

            Primitive primitive = SpawnPrimitiveFromStatus(player, status);

            PlayerStatuses[player.UserId].Add(new StatusInfo(player, status, primitive));
        }

        public static void EnableRandom(Player player)
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();
            }

            var status = RegisteredStatuses.GetRandomValue();

            if (Contains(player, status))
            {
                return;
            }

            var primitive = SpawnPrimitiveFromStatus(player, status);

            PlayerStatuses[player.UserId].Add(new StatusInfo(player, status, primitive));
        }

        public static void EnableRandom(Player player, CategoryType categoryType)
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();
            }

            var status = RegisteredStatuses.GetRandomValue(_status => _status.Category == categoryType);

            if (Contains(player, status))
            {
                return;
            }

            var primitive = SpawnPrimitiveFromStatus(player, status);

            PlayerStatuses[player.UserId].Add(new StatusInfo(player, status, primitive));
        }

        public static void Disable<T>(Player player) where T : IStatus, new()
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();

                return;
            }

            if (!Contains<T>(player))
            {
                return;
            }

            var status = new T();
            var primitive = PlayerStatuses[player.UserId].FirstOrDefault(_status => _status.Status.Name == status.Name).Primitive;

            PlayerStatuses[player.UserId].Remove(PlayerStatuses[player.UserId].FirstOrDefault(_status => _status.Status.Name == status.Name));
            primitive.Destroy();
        }

        public static bool Contains<T>(Player player) where T : IStatus, new()
        {
            return Contains(player, new T());
        }

        public static bool Contains(Player player, IStatus status)
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();

                return false;
            }

            if (PlayerStatuses[player.UserId].FirstOrDefault(_status => _status.Status.Name == status.Name) == default)
            {
                return false;
            }

            return true;
        }

        public static bool Clear(Player player)
        {
            if (!PlayerStatuses.ContainsKey(player.UserId))
            {
                PlayerStatuses[player.UserId] = new List<StatusInfo>();

                return false;
            }

            PlayerStatuses[player.UserId].Clear();

            return true;
        }

        public static List<StatusInfo> GetPlayerStatuses(Player player)
        {
            return new List<StatusInfo>(PlayerStatuses[player.UserId]);
        }

        public static List<IStatus> GetStatuses()
        {
            return new List<IStatus>(RegisteredStatuses);
        }

        private static Vector3 GetPosition(Player player)
        {
            var count = PlayerStatuses[player.UserId].Count;

            return GetPosition(player, count);
        }

        public static Vector3 GetPosition(Player player, int count)
        {
            Vector3 position = player.CameraTransform.position;

            return new Vector3(position.x, position.y + 0.5f + (0.5f * count), position.z);
        }

        private static float GetRotation(Player player)
        {
            if (PlayerStatuses[player.UserId].Count / 2 == 0)
            {
                return RotationSpeed;
            }

            return -RotationSpeed;
        }

        public static Primitive SpawnPrimitiveFromStatus(Player player, IStatus status)
        {
            Primitive primitive = Primitive.Create(GetPosition(player), Vector3.zero, new Vector3(-0.2f, -0.2f, -0.2f), false);
            primitive.Type = status.Primitive;
            primitive.Color = status.Color;
            primitive.AdminToyBase.gameObject.AddComponent<StatusObject>().Initialization(player, status, GetRotation(player), PlayerStatuses[player.UserId].Count);
            primitive.Spawn();

            return primitive;
        }
    }
}
