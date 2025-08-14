using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hostal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsVip = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indicates if the client has VIP status for special benefits"),
                    Email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: true, comment: "Client's email address for communication and reservations"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete flag - true means active, false means deleted"),
                    CI = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, comment: "Carnet de Identidad - 11 digit national identification number"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "First name of the person"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Last name of the person"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Contact phone number")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeadHousekeepers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete flag - true means active, false means deleted"),
                    CI = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, comment: "Carnet de Identidad - 11 digit national identification number"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "First name of the person"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Last name of the person"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Contact phone number")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadHousekeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "Room number following format 0XY where X is floor (1-3) and Y is room number (1-5)"),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Maximum number of occupants the room can accommodate"),
                    IsOutOfService = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Soft delete flag - true means active, false means deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaReservacion = table.Column<DateTime>(type: "smalldatetime", nullable: false, defaultValueSql: "GETDATE()", comment: "Fecha en que se realizó la reserva."),
                    FechaEntrada = table.Column<DateTime>(type: "smalldatetime", nullable: false, comment: "Fecha de inicio de la estancia del cliente."),
                    FechaSalida = table.Column<DateTime>(type: "smalldatetime", nullable: false, comment: "Fecha de fin de la estancia del cliente."),
                    ImporteRenta = table.Column<decimal>(type: "decimal(10, 2)", nullable: false, comment: "Costo total de la reserva."),
                    EstaElClienteEnHostal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indica si el cliente ha realizado el check-in (true) o no (false)."),
                    EstaCancelada = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Indica si la reserva fue cancelada (true) o no (false)."),
                    FechaCancelacion = table.Column<DateTime>(type: "smalldatetime", nullable: true, comment: "Fecha en que se canceló la reserva. Nulo si no está cancelada."),
                    MotivoCancelacion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "Motivo especificado por el cual se canceló la reserva."),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.CheckConstraint("CK_Reservation_AmountPositive", "[ImporteRenta] >= 0");
                    table.CheckConstraint("CK_Reservation_DateOrder", "[FechaSalida] > [FechaEntrada]");
                    table.ForeignKey(
                        name: "FK_Reservations_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Room",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomHeadHousekeepers",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    HeadHousekeeperId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomHeadHousekeepers", x => new { x.RoomId, x.HeadHousekeeperId });
                    table.ForeignKey(
                        name: "FK_RoomHeadHousekeepers_HeadHousekeepers_HeadHousekeeperId",
                        column: x => x.HeadHousekeeperId,
                        principalTable: "HeadHousekeepers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomHeadHousekeepers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_FullName",
                table: "Clients",
                columns: new[] { "Name", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Phone",
                table: "Clients",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CI",
                table: "Clients",
                column: "CI",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HeadHousekeeper_FullName",
                table: "HeadHousekeepers",
                columns: new[] { "Name", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_HeadHousekeeper_Phone",
                table: "HeadHousekeepers",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientId",
                table: "Reservations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DateRange",
                table: "Reservations",
                columns: new[] { "FechaEntrada", "FechaSalida" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomHeadHousekeepers_HeadHousekeeperId",
                table: "RoomHeadHousekeepers",
                column: "HeadHousekeeperId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomHeadHousekeepers_RoomId",
                table: "RoomHeadHousekeepers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "RoomHeadHousekeepers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "HeadHousekeepers");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
