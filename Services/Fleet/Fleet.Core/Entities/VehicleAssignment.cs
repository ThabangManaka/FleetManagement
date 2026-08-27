using System;
using System.Collections.Generic;
using System.Text;

namespace Fleet.Core.Entities
{
    public class VehicleAssignment
    {
        public Guid Id { get; private set; }

        public Guid VehicleId { get; private set; }

        public Guid DriverId { get; private set; }

        public DateTime AssignedAt { get; private set; }

        public DateTime? UnassignedAt { get; private set; }

        public Vehicle Vehicle { get; private set; } = null!;

        public Driver Driver { get; private set; } = null!;

        private VehicleAssignment()
        {
        }

        public VehicleAssignment(
            Guid vehicleId,
            Guid driverId)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId;
            DriverId = driverId;
            AssignedAt = DateTime.UtcNow;
        }

        public void Unassign()
        {
            UnassignedAt = DateTime.UtcNow;
        }

        public bool IsActive()
        {
            return UnassignedAt == null;
        }
    }