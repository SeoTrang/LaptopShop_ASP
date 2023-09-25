﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapTopShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCountToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "CartItems");
        }
    }
}
