/*
 * The Cart
 * Copyright (c) 2026 Carter Games
 *
 * This program is free software: you can redistribute it and/or modify it under the terms of the
 * GNU General Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version. 
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
 *
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <https://www.gnu.org/licenses/>. 
 */

namespace CarterGames.Cart.Crates.NotionData.Editor
{
    public class CrateNotionData : ExternalCrate
    {
        public override string CrateName => "Notion Data";
        public override string CrateDescription =>
            "A tool to download Notion databases into a Unity scriptable object for use in Unity projects. Handy for game data such as items, localization, skills and more!";
        
        public override string CrateAuthor => CrateConstants.CarterGamesAuthor;

        public override string CrateDefine => "CARTERGAMES_CART_CRATE_NOTIONDATA";
        
        public override GitPackageInfo PackageInfo => new GitPackageInfo("Notion Data", "games.carter.notiondata",
            "https://github.com/CarterGames/NotionToUnity.git");
    }
}