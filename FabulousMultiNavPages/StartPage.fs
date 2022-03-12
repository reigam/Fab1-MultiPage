namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module StartPage =
    type Model = { Title: string }

    type Msg =
        | OpenPage of string

    let initModel = { Title = "Start Page" }

    let init() = initModel, Cmd.none

    let update msg (model: Model) (globalModel: GlobalModel) =
        match msg with
        | OpenPage s -> model, { globalModel with PageStash = List.append globalModel.PageStash [s] }, Cmd.none

    let view (model: Model) (globalModel: GlobalModel) dispatch =
        View.ContentPage (
            title = model.Title,
            content = View.StackLayout (                
                verticalOptions = LayoutOptions.Center,
                children = [
                    View.Button (
                        text = "Go to first page",
                        command = (fun () -> dispatch (OpenPage "FirstPage")),
                        horizontalOptions = LayoutOptions.Center) 
                    View.Button (
                        text = "Go to second page",
                        command = (fun () -> dispatch (OpenPage "SecondPage")),
                        horizontalOptions = LayoutOptions.Center) 
                ]))