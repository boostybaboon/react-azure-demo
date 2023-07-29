# React Azure Demo

Project to learn how to develop a web application using React for the front end and a REST api for the back end, deployed as an Azure Static Web App with source control in GitHub

1. Following this tutorial - "[Azure Static Web Apps](https://docs.microsoft.com/azure/static-web-apps/overview) allows you to easily build [React](https://reactjs.org/) apps in minutes. Use this repo with the [React quickstart](https://docs.microsoft.com/azure/static-web-apps/getting-started?tabs=react) to build and customize a new static site.
   This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app)."
2. Following [here](https://learn.microsoft.com/en-gb/azure/static-web-apps/add-api?tabs=reacthttps://)  but also did '''npm audit fix --force''' inbetween npm install and npm run build otherwise errors in build (see this [answer](https://stackoverflow.com/a/73027407) here
3. Deleted api folder, added an api based on c#
4. To get it to display in the main window, changed c# code to return .json content like so:
   1. following answer on [stack overflow](https://stackoverflow.com/a/73407044) , just do return JsonResult(data), data can be a dynamic object
