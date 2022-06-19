using Android.Runtime;
using Android.Views;
using Android.Widget;
using GridView.Adapter;
using GridView.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace GridView
{
    public class ViewGrid : LinearLayout
    {
        private List<object> ObjData;
        private LinearLayout GridEncabezado;
        private ListView GridList;
        private Android.Content.Context con;
        private List<ImagenView> ImgList;
        private Android.Graphics.Color HeaderColor;
        private Android.Graphics.Color CellsColor;
        private Android.Graphics.Color SeparatorColor;
        private Android.Graphics.Color TintIconOrder;
        private bool EncabezadoOrderBy;

        public ViewGrid(Android.Content.Context con, Android.Util.IAttributeSet attr) : base(con, attr)
        {
            this.con = con;
            var Attributes = con.ObtainStyledAttributes(attr, Resource.Styleable.GridView);
            HeaderColor = Attributes.GetColor(Resource.Styleable.GridView_HeaderColor, con.GetColor(Resource.Color.DefaultHeaderColor));
            CellsColor = Attributes.GetColor(Resource.Styleable.GridView_CellsColor, con.GetColor(Resource.Color.DefaultCellsColor));
            SeparatorColor = Attributes.GetColor(Resource.Styleable.GridView_SeparatorColor, con.GetColor(Resource.Color.DefaultSeparatorColor));
            TintIconOrder = Attributes.GetColor(Resource.Styleable.GridView_TintIconOrder, con.GetColor(Resource.Color.DefaultTintIconOrderColor));
            EncabezadoOrderBy = Attributes.GetBoolean(Resource.Styleable.GridView_EncabezadoOrderBy, false);
            ImgList = new List<ImagenView>();
            initview(con);
        }
        public void SetData(IList ObjData)
        {
            if (ObjData != null && ObjData.Count > 0)
            {
                this.ObjData = new List<object>(ObjData.OfType<object>());
                GridEncabezado.SetBackgroundColor(SeparatorColor);
                AddEncabezado();
                AddRows(true); 
            }
        }
        private void AddEncabezado()
        {
            int count = 0;
            int size = ObjData[0].GetType().GetProperties().Length-1;
            foreach (PropertyInfo Property in ObjData[0].GetType().GetProperties())
            {           
                var Texto = ObjData[0].GetType().GetProperty(Property.Name).GetValue(ObjData[0], null).ToString();
                GridEncabezado.AddView(CreateTextView(count, size, Texto));
                count++;
            }       
        }
        private void AddRows(bool delete)
        {
            if(delete)
                ObjData.RemoveAt(0);
            AdapterViewGrid agv = new AdapterViewGrid(con, ObjData, CellsColor, SeparatorColor);
            GridList.Adapter = agv;
        }
        private View CreateTextView(int count, int size, string Texto)
        {
            LayoutParams parameterll1 = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            LinearLayout ll = new LinearLayout(con);
            parameterll1.Weight = 1;
            ll.LayoutParameters = parameterll1;
            View inflates = Inflate(con, Resource.Layout.recurso_encabezadoitem, ll);
            TextView txt = inflates.FindViewById<TextView>(Resource.Id.TextViewEncabezado);
            ImageView img = inflates.FindViewById<ImageView>(Resource.Id.ImgEncabezado);
            LinearLayout llEncabezado = inflates.FindViewById<LinearLayout>(Resource.Id.LlEncabezado);

            ImgList.Add(new ImagenView { Img = img,Estado = 0 });

            if (size == count)
            {
                LayoutParams parameterll2 = new LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                llEncabezado.LayoutParameters = parameterll2;
            }
            img.SetColorFilter(TintIconOrder);
            llEncabezado.SetBackgroundColor(SeparatorColor);
            txt.SetBackgroundColor(HeaderColor);

            txt.Text = Texto;

            switch (EncabezadoOrderBy) 
            {
                case true:
                    ll.Click += (sender, args) => { ClickEventEncabezado(count); };
                    break;
                case false:
                    img.SetImageBitmap(null);
                    break;
            }

            return inflates;
        }

        private void ClickEventEncabezado(int position)
        {
            bool AscDesc = ControlStateImgOrder(position);

            switch (AscDesc) 
            {
                case true:
                    ObjData = ObjData.OrderByDescending(x => x.GetType().GetProperties()[position].GetValue(x)).ToList();
                    break;
                case false:
                    ObjData = ObjData.OrderBy(x => x.GetType().GetProperties()[position].GetValue(x)).ToList();
                    break;
            }
            AddRows(false);
        }
        private bool ControlStateImgOrder(int position)
        {
            bool AscDesc = true;
            switch (ImgList[position].Estado)
            {
                case 0:
                    ImgList[position].Img.SetImageResource(Resource.Drawable.down);
                    ImgList[position].Estado = 1;
                    AscDesc = true;
                    break;
                case 1:
                    ImgList[position].Img.SetImageResource(Resource.Drawable.up);
                    ImgList[position].Estado = 0;
                    AscDesc = false;
                    break;
            }
            for (int i = 0; i <= ImgList.Count-1; i++) 
            {
                if (i != position) 
                {
                    ImgList[i].Img.SetImageResource(Resource.Drawable.line);
                    ImgList[i].Estado = 0;
                }
            }
            return AscDesc;
        }
        private void initview(Android.Content.Context con)
        {
            Inflate(con, Resource.Layout.view_gridview,this);
            GridEncabezado = FindViewById<LinearLayout>(Resource.Id.GridEncabezado);
            GridList = FindViewById<ListView>(Resource.Id.GridList);
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            for (int i = 0; i < ChildCount; i++)
            {
                GetChildAt(i).Layout(l, t, r, b);
            }
        }
    }
}