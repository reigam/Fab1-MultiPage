namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module FirstPage =
    type Model = { Title: string }
      
    type Msg =
        | OpenPage of string
        | Close

    let initModel = { Title = "First Page" }    

    let init() = initModel, Cmd.none

    let update msg (model: Model) (globalModel: GlobalModel) =
        match msg with
        | OpenPage s -> model, { globalModel with PageStash = List.append globalModel.PageStash [s] }, Cmd.none
        | Close -> model, { globalModel with PageStash = ["WelcomePage"] }, Cmd.none

    let view (model: Model) (globalModel: GlobalModel) dispatch =
        View.ContentPage (
            title = model.Title,
            content = View.StackLayout (
                verticalOptions = LayoutOptions.Center,
                children = [
                    View.Button (
                        text = "Go to second page",
                        command = (fun () -> dispatch (OpenPage "SecondPage")),
                        horizontalOptions = LayoutOptions.Center) 

                ]))