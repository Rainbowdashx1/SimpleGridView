using Android.Views;
using Android.Widget;
using System;
using System.Collections;
using System.Reflection;


namespace GridView.Adapter
{
    public class AdapterViewGrid : BaseAdapter<object>
    {
        private Android.Content.Context context;
        public IList modelsArrayListparam;
        public IList OrimodelsArrayListparam;
        private Android.Graphics.Color CellsColor;
        private Android.Graphics.Color SeparatorColor;
        public AdapterViewGrid(Android.Content.Context context, IList modelsArrayListparam, Android.Graphics.Color CellsColor, Android.Graphics.Color SeparatorColor)
        {
            this.context = context;
            this.CellsColor = CellsColor;
            this.SeparatorColor = SeparatorColor;
            this.modelsArrayListparam = modelsArrayListparam;
            this.OrimodelsArrayListparam = modelsArrayListparam;
        }
        public override object this[int position] => throw new NotImplementedException();

        public void resetData()
        {
            this.modelsArrayListparam = OrimodelsArrayListparam;
        }
        public object getObject(int posicion)
        {
            return modelsArrayListparam[posicion];
        }
        public override int Count
        {
            get
            {
                return this.modelsArrayListparam.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return modelsArrayListparam[position].GetHashCode();
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View rowView = convertView;
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Android.Content.Context.LayoutInflaterService);
            rowView = inflater.Inflate(Resource.Layout.recurso_gridrowelement, parent, false);
            LinearLayout ll = rowView.FindViewById<LinearLayout>(Resource.Id.GridViewRowElement);
            ll.SetBackgroundColor(SeparatorColor);
            rowView.SetBackgroundColor(SeparatorColor);

            int size = modelsArrayListparam[0].GetType().GetProperties().Length - 1;
            int count = 0;
            foreach (PropertyInfo Property in modelsArrayListparam[position].GetType().GetProperties())
            {
                var Texto = modelsArrayListparam[position].GetType().GetProperty(Property.Name).GetValue(modelsArrayListparam[position], null).ToString();
                TextView TxtV = CreateTextView(count, size, Texto);
                ll.AddView(TxtV);
                count++;
            }

            return rowView;
        }
        private TextView CreateTextView(int count,int size, string Texto)
        {
            TextView TxtV = new TextView(context);
            LinearLayout.LayoutParams paramsa = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            if (size != count)
                paramsa.RightMargin = 1;
            paramsa.Weight = 1;
            TxtV.LayoutParameters = paramsa;
            TxtV.Text = Texto;
            TxtV.Gravity = GravityFlags.CenterVertical | GravityFlags.CenterVertical | GravityFlags.Center;
            TxtV.SetBackgroundColor(CellsColor);
            return TxtV;
        }
    }
}