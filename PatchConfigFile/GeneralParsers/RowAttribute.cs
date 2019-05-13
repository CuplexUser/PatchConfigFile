using System;
using System.Collections;
using System.Collections.Generic;

namespace PatchConfigFile.GeneralParsers
{
   public class RowAttribute : IEnumerable<RowAttribute>, IEnumerator, IComparable<RowAttribute>, IComparer, IComparer<RowAttribute>
   {
      private int _enumIndex = 0;

      public RowAttribute()
      {

      }

      public RowAttribute(RowAttribute nextRow)
      {
         NextRow = nextRow;
      }

      public RowAttribute(RowAttribute nextRow, RowAttribute previousRow)
      {
         NextRow = nextRow;
         PreviousRow = previousRow;
      }

      public int RowNumber { get; set; }

      public int RowTrimmedLength { get; set; }

      public string RowText { get; set; }

      public RowAttribute NextRow { get; private set; }

      public RowAttribute PreviousRow { get; private set; }

      public bool SetNextRow(RowAttribute next)
      {
         if (next == null || next.RowNumber != RowNumber + 1)
         {
            return false;
         }

         NextRow = next;
         return true;
      }

      public bool SetPreviousRow(RowAttribute previous)
      {
         if (previous?.RowNumber < 0)
            return false;

         if (previous == null || previous.RowNumber != RowNumber - 1)
         {
            return false;
         }

         PreviousRow = previous;
         return true;
      }


      public IEnumerator<RowAttribute> GetEnumerator()
      {
         SortedSet<RowAttribute> generatedSet = GenerateSortedSet();

         return generatedSet.GetEnumerator();
      }

      private SortedSet<RowAttribute> GenerateSortedSet()
      {
         List<RowAttribute> rowAttrList = new List<RowAttribute>();

         rowAttrList.Add(this);
         if (this.NextRow != null)
         {
            rowAttrList.AddRange(GetDescending(this, RowNumber));
         }

         if (this.NextRow != null)
         {
            rowAttrList.AddRange(GetAscending(this, RowNumber));
         }

         SortedSet<RowAttribute> generatedSet = new SortedSet<RowAttribute>(rowAttrList);
         return generatedSet;
      }

      private List<RowAttribute> GetAscending(RowAttribute current, int rowNumber)
      {
         if (current == null)
            return null;

         List<RowAttribute> rowList = new List<RowAttribute>(current);

         while (current.NextRow != null)
         {
            rowList.AddRange(GetAscending(current.NextRow, current.RowNumber + 1));
         }


         return rowList;
      }

      private List<RowAttribute> GetDescending(RowAttribute current, int rowNumber)
      {
         if (current == null)
            return null;

         List<RowAttribute> rowList = new List<RowAttribute>(current);

         while (current.PreviousRow != null)
         {
            rowList.AddRange(GetDescending(current.PreviousRow, current.RowNumber - 1));
         }


         return rowList;
      }


      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      public bool MoveNext()
      {
         if (NextRow != null)
         {
            _enumIndex = NextRow.RowNumber;
            return true;
         }

         return false;

      }

      public void Reset()
      {
         _enumIndex = 0;
      }

      public object Current => this;

      public int CompareTo(RowAttribute other)
      {
         if (ReferenceEquals(this, other))
         {
            return 0;
         }

         if (ReferenceEquals(null, other))
         {
            return 1;
         }

         var enumIndexComparison = _enumIndex.CompareTo(other._enumIndex);
         if (enumIndexComparison != 0)
         {
            return enumIndexComparison;
         }

         var rowNumberComparison = RowNumber.CompareTo(other.RowNumber);
         if (rowNumberComparison != 0)
         {
            return rowNumberComparison;
         }

         var rowTrimmedLengthComparison = RowTrimmedLength.CompareTo(other.RowTrimmedLength);
         if (rowTrimmedLengthComparison != 0)
         {
            return rowTrimmedLengthComparison;
         }

         var nextRowComparison = Comparer<RowAttribute>.Default.Compare(NextRow, other.NextRow);
         if (nextRowComparison != 0)
         {
            return nextRowComparison;
         }

         return Comparer<RowAttribute>.Default.Compare(PreviousRow, other.PreviousRow);
      }

      public int Compare(object x, object y)
      {
         if (x == null && y == null)
            return 0;

         if (x == null)
            return -1;

         if (y == null)
            return 1;

         if (x is RowAttribute attribute && y is RowAttribute attribute2)
         {
            bool compOrdinal = attribute.RowNumber > attribute2.RowNumber;
            if (attribute.RowNumber == attribute2.RowNumber)
            {
               return 0;
            }

            return compOrdinal ? 1 : -1;

         }

         return 0;
      }

      public int Compare(RowAttribute x, RowAttribute y)
      {
         switch (x)
         {
            case null when y == null:
               return 0;
            case null:
               return -1;
         }

         if (y == null)
            return 1;


         bool compOrdinal = x.RowNumber > y.RowNumber;

         if (x.RowNumber == y.RowNumber)
         {
            return 0;
         }

         return compOrdinal ? 1 : -1;
      }
   }
}