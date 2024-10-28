using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Save;

namespace CarterGames.TheCart.Core.Editor
{
	public class SearchProviderSaveMethod : SearchProvider<ISaveMethod>
	{
		private static SearchProviderSaveMethod Instance;
		
		public override string ProviderTitle => "Select Save Method";
		
		
		public override List<SearchGroup<ISaveMethod>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<ISaveMethod>>();
			var entries = new List<SearchItem<ISaveMethod>>();
			var options = AssemblyHelper.GetClassesOfType<ISaveMethod>(false).Where(t => !ToExclude.Contains(t));

			foreach (var entry in options)
			{
				entries.Add(SearchItem<ISaveMethod>.Set(entry.GetType().Name, entry));
			}
			
			list.Add(new SearchGroup<ISaveMethod>(string.Empty, entries));
			
			return list;
		}
		
		
		public static SearchProviderSaveMethod GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderSaveMethod>();
			}

			return Instance;
		}
	}
}