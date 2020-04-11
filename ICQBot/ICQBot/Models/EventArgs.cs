using System;
using System.Collections.Generic;
using System.Text;

namespace ICQBot.Models
{
    public class ICQEventArgs<T>
    {
        public T Event { get; set; }
    }
}
