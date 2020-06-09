# (My) Google Calendar API 

API to create/edit/delete personalized calendar events.
It is also a gateway to [G Calendar API](https://developers.google.com/calendar), which is used as cloud db storage for the events, and also to experience the GCA functionalities (for example: set event notifications in phone and email).

## Pre-requisites
Create a GCP project, nerate OAuth2.0 authentication, download credentials and copy them in root folder with the name "credentials.json"

[GCP project](https://cloud.google.com/resource-manager/docs/creating-managing-projects)

## Initialization

When running the project for the first time, __Quickstart__ will launch your browser asking for access. Join with the @gmail account that you wanna use to store the events and allow permissions.

Navigate to `https://localhost:5001/swagger/index.html` and explore the methods and models.

## Deployment

Install API in IIS Service and integrate with your interface
