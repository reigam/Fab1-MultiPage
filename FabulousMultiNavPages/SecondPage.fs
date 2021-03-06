namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module SecondPage =
    let thisPage = AppPages.names.SecondPage
    type Model = { 
        Title: AppPages.Name 
    }
      
    type Msg =
        | Close

    let initModel = { Title = thisPage }

    let init() = initModel, Cmd.none

    let update msg (model: Model) (globalModel: GlobalModel) =
        match msg with
        | Close -> model, { globalModel with PageStash = [AppPages.names.StartPage] }, Cmd.none

    let view (model: Model) (globalModel: GlobalModel) dispatch =
        View.ContentPage (
            title = (model.Title |> AppPages.nameValue),
            content = View.StackLayout (
                verticalOptions = LayoutOptions.Center,
                children = [
                    View.Button (
                        text = "Back to start page",
                        command = (fun () -> dispatch (Close)),
                        horizontalOptions = LayoutOptions.Center) ]))