namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module FirstPage =
    let thisPage = AppPages.names.FirstPage
    type Model = { 
        Title: AppPages.Name 
    }
      
    type Msg =
        | OpenPage of AppPages.Name
        | Close

    let initModel = { Title = thisPage }    

    let init() = initModel, Cmd.none

    let update msg (model: Model) (globalModel: GlobalModel) =
        match msg with
        | OpenPage s -> model, { globalModel with PageStash = List.append globalModel.PageStash [s] }, Cmd.none
        | Close -> model, { globalModel with PageStash = [AppPages.names.StartPage] }, Cmd.none

    let view (model: Model) (globalModel: GlobalModel) dispatch =
        View.ContentPage (
            title = (model.Title |> AppPages.nameValue),
            content = View.StackLayout (
                verticalOptions = LayoutOptions.Center,
                children = [
                    View.Button (
                        text = "Go to second page",
                        command = (fun () -> dispatch (OpenPage AppPages.names.SecondPage)),
                        horizontalOptions = LayoutOptions.Center) 

                ]))