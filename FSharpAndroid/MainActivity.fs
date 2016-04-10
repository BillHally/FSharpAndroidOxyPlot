namespace FSharpAndroid

open System

open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget

open OxyPlot
open OxyPlot.Series
open OxyPlot.Xamarin.Android

[<Activity (Label = "FSharpAndroid", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    let mutable count:int = 1

    override this.OnCreate (bundle) =

        base.OnCreate (bundle)

        // Set our view from the "main" layout resource
        this.SetContentView (Resource_Layout.Main)

        // Get our button from the layout resource, and attach an event to it
        let button = this.FindViewById<Button>(Resource_Id.MyButton)
        button.Click.Add (fun args -> 
            button.Text <- sprintf "%d clicks!" count
            count <- count + 1
        )

        let points = [0.0..0.01..(2.0 * Math.PI)] |> List.map (fun x -> DataPoint(x, sin x))
        let series = LineSeries(StrokeThickness = 1.0, Color = OxyColors.Red, Smooth = true)
        series.Points.AddRange(points)
        let model = PlotModel()
        model.Series.Add(series)
        let plotView = this.FindViewById<PlotView>(Resource_Id.PlotView)
        plotView.Model <- model       

