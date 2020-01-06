# MovieApi

Within the main folder you will see the test solution and a html page.
The html page is very basic, this was an attempt at a HTTPRequest from a webpage and only brings back json from one endpoint. It can be tested by using localhost and changing the url within the html page to your localhost port.

Please note - As this is being run from a local machine you will run into cors errors, in a realworld scenraio it would be much better practice to host the file. I used a version of chrome with web security disabled:
chrome.exe --user-data-dir="C:/Chrome dev session" --disable-web-security
