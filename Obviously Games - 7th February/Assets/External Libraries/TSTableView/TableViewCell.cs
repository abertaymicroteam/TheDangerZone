﻿using UnityEngine;
using System.Collections;
using System;

namespace Tacticsoft
{
    /// <summary>
    /// The base class for cells in a TableView. ITableViewDataSource returns pointers
    /// to these objects
    /// </summary>
	[CLSCompliant(false)]
    public class TableViewCell : MonoBehaviour
    {
        /// <summary>
        /// TableView will cache unused cells and reuse them according to their
        /// reuse identifier. Override this to add custom cache grouping logic.
        /// </summary>
        public virtual string reuseIdentifier { 
            get { 
                return this.GetType().Name; 
            } 
        }
    }
}
