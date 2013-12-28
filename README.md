Skitch to Azure
==========================

##Project Description

A small ASP.NET Web API project that allows [Skitch](http://evernote.com/skitch/) to post screenshots via FTP and then automatically store in [Windows Azure Blob Storage](http://www.windowsazure.com/en-us/develop/net/how-to-guides/blob-storage/).

##What's the Problem?

Skitch, specifcally pre-Evernote versions, would allow screenshots to be captured and uploaded to an FTP location. However, one of the requirements was that it would then be accessible via HTTP immediately after upload (when the FTP upload finishes, an HTTP HEAD request is made to your new link checking for HTTP 200); if any redirection or movement occurred, especially if it was not instant, Skitch would report that the upload failed and was not accessible. This project solves that.

##How Does It Work?

__TODO__
