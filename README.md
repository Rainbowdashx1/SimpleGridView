# SimpleGridView V1.0.0
Xamarin android Grid

[![Version](https://img.shields.io/nuget/v/SimpleGridView)](https://www.nuget.org/packages/SimpleGridView/)
[![License](https://img.shields.io/github/license/Rainbowdashx1/SimpleGridView)](https://github.com/Rainbowdashx1/SimpleGridView/blob/master/LICENSE.md)

# Uso
Objeto con el que trabajaremos 
* `GridView.ViewGrid` (Compatible version API >= 23) 

Código de ejemplo : 

```xml
<?xml version="1.0" encoding="utf-8"?>
<RelativeLayout xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:app="http://schemas.android.com/apk/res-auto"
    xmlns:tools="http://schemas.android.com/tools"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    app:layout_behavior="@string/appbar_scrolling_view_behavior"
    tools:showIn="@layout/activity_main">

    <GridView.ViewGrid
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        android:id="@+id/viewgridone"
        app:SeparatorColor="#b30000"
        app:HeaderColor="#c4c4ff"
        app:CellsColor="#d8ffd8"
        app:EncabezadoOrderBy="false"/>

</RelativeLayout>
```
### Instancia GridView
`GridView.ViewGrid` una vez declarado en xml(vista) se puede instanciar como cualquier otro componente android 
Ejemplo: 
```csharp
public class Activity1 : AppCompatActivity
{
    GridView.ViewGrid GVG;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);
        GVG = FindViewById<GridView.ViewGrid>(Resource.Id.viewgridone);
    }
}
```
### Agregar datos a la grilla
Puede usar el método `SetData` para agregar un `List` del objeto que necesite mostrar la grilla.
La primera posición del objeto se usará para el encabezado y el resto de la lista se usará para formar la grilla.
Código de referencia:
```csharp
namespace Test
{
    public class ObjetoTest
    {
        public string Dato1 { get; set; }
        public string Dato2 { get; set; }
        public string Dato3 { get; set; }
        public string Dato4 { get; set; }
    }
}
```
```csharp
public class Activity1 : AppCompatActivity
{
    GridView.ViewGrid GVG;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);

        List<ObjetoTest> tt = new List<ObjetoTest>();

        tt.Add(new ObjetoTest { Dato1 = "A", Dato2 = "DATA2", Dato3 = "DATA3", Dato4 = "DATA4" });
        tt.Add(new ObjetoTest { Dato1 = "B", Dato2 = "DATA2", Dato3 = "DATA3", Dato4 = "DATA4" });
        tt.Add(new ObjetoTest { Dato1 = "C", Dato2 = "DATA2", Dato3 = "DATA3", Dato4 = "DATA4" });
        tt.Add(new ObjetoTest { Dato1 = "D", Dato2 = "DATA2", Dato3 = "DATA3", Dato4 = "DATA4" });
        tt.Add(new ObjetoTest { Dato1 = "E", Dato2 = "DATA2", Dato3 = "DATA3", Dato4 = "DATA4" });

        GVG = FindViewById<GridView.ViewGrid>(Resource.Id.viewgridone);
        GVG.SetData(tt);
    }
}
```

### Personalización de la grilla

* `SeparatorColor` Da color a la separación que hay entre filas y columnas (por defecto negro)
* `HeaderColor` cambia el color que tendrá el encabezado de la grilla (por defecto gris(#e5e5e5)).
* `CellsColor` Cambia el color que tendrán las celdas que no son parte del encabezado(por defecto blanco).
* `EncabezadoOrderBy` Activa o desactiva evento click del encabezado que permite ordenar de forma ascendiente y descendiente(por defecto false).
