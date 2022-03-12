// Copyright Fabulous contributors. See LICENSE.md for license.
namespace FabulousMultiNavPages

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms

module App =
    type Model =
      { Global: GlobalModel
        StartPage: StartPage.Model
        FirstPage: FirstPage.Model
        SecondPage: SecondPage.Model }

    type Msg =
        | StartPageMsg of StartPage.Msg
        | FirstPageMsg of FirstPage.Msg
        | SecondPageMsg of SecondPage.Msg
        | NavigationPopped

    let initModel =
        { Global = { 
            PageStash = ["StartPage"] }
          StartPage = fst (StartPage.init())
          FirstPage = fst (FirstPage.init())
          SecondPage = fst (SecondPage.init()) }

    let init() = initModel, Cmd.none

    let update msg (model:Model) =
        match msg with
        | StartPageMsg m ->
            let l, g, c = StartPage.update m model.StartPage model.Global
            { model with StartPage = l; Global = g }, (Cmd.map StartPageMsg c)
        | FirstPageMsg m ->
            let l, g, c = FirstPage.update m model.FirstPage model.Global
            { model with FirstPage = l; Global = g }, (Cmd.map FirstPageMsg c)
        | SecondPageMsg m ->
            let l, g, c = SecondPage.update m model.SecondPage model.Global
            { model with SecondPage = l; Global = g }, (Cmd.map SecondPageMsg c)
        | NavigationPopped ->
            { model with Global = {PageStash = Helpers.reshuffle model.Global.PageStash }}, Cmd.none
        
    let view (model: Model) dispatch =
        View.NavigationPage(pages = [            
            for page in model.Global.PageStash do
                match page with
                | "StartPage" -> 
                    let p = StartPage.view model.StartPage model.Global (StartPageMsg >> dispatch)
                    yield p
                | "FirstPage" ->   
                    let p = FirstPage.view model.FirstPage model.Global (FirstPageMsg >> dispatch)
                    yield p
                | "SecondPage" -> 
                    let p = SecondPage.view model.SecondPage model.Global (SecondPageMsg >> dispatch)
                    yield p
                | _ -> ()
        ], popped = fun _ -> dispatch NavigationPopped)

    //let program = Program.mkProgram init update view

    // Note, this declaration is needed if you enable LiveUpdate
    let program =
        XamarinFormsProgram.mkProgram init update view
#if DEBUG
        |> Program.withConsoleTrace
#endif

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/tools.html#live-update for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/models.html#saving-application-state for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


