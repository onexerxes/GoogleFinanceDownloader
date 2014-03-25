﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleFinanceLibrary {	
	public class Tick : IEquatable<Tick>, IComparable<Tick> {
		// Public properties
		public string SymbolWithExchange { get; private set; }
		public DateTime Date {get; private set;}
		public double OpenPrice {get; private set;}
		public double HighPrice { get; private set; }
		public double LowPrice { get; private set; }
		public double ClosePrice { get; private set; }
		public double Volume { get; private set; }
		public Tick LastTick { get; set; }
		
		// Factory method
		public static Tick FromStringArray(string exchange, string symbol, string[] data) {
			return new Tick {
				SymbolWithExchange = exchange + ":" + symbol,
				Date = DateTime.Parse(data[0]),
				OpenPrice = float.Parse(data[1]),
				HighPrice = float.Parse(data[2]),
				LowPrice = float.Parse(data[3]),
				ClosePrice = float.Parse(data[4]),
				Volume = float.Parse(data[5])
			};
		}
		
		// Public methods
		public double GetChangePercent(bool sinceLastClose) {
			double lastPrice = (sinceLastClose && LastTick != null) ? LastTick.ClosePrice : OpenPrice;

			return Math.Round((ClosePrice - lastPrice) / lastPrice * 100, 2);
		}

		#region IEquatable<Tick> Members

		public bool Equals(Tick other) {
			return ((this.SymbolWithExchange == other.SymbolWithExchange) && (this.Date == other.Date));
		}

		#endregion

		#region IComparable<Tick> Members

		public int CompareTo(Tick other) {
			return this.Date.CompareTo(other.Date);
		}

		#endregion
	}
}
