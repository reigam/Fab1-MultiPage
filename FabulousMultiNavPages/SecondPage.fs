namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module SecondPage =
    type Model =
      { Title: string }
      
    type Msg =
        | Close

    let initModel = { Title = "Second Page" }

    let init() = initModel, Cmd.none

    let update msg (model: Model) (globalModel: GlobalModel) =
        match msg with
        | Close -> model, { globalModel with PageStash = ["StartPage"] }, Cmd.none

    let view (model: Model) (globalModel: GlobalModel) dispatch =
        View.ContentPage (
            title = model.Title,
            content = View.StackLayout (
                verticalOptions = LayoutOptions.Center,
                children = [
                    View.Button (
                        text = "Back to start page",
                        command = (fun () -> dispatch (Close)),
                        horizontalOptions = LayoutOptions.Center) ]))