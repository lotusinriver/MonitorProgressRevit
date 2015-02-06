# MonitorProgressRevit
Monitor Progress While Call Revit API 


According to this blog:
http://thebuildingcoder.typepad.com/blog/2014/05/multithreading-throws-exceptions-in-revit-2015.html

Multi-treaded use of the Revit API is not supported since Revit 2015.
Which means Revit API call is not allowed to start in BackGroundWorker

Here is a workaround to monitor the progress in a modal dialog while running Revit API call at the same time.

Two tricks:

1)Start Revit API call when windows.forms.shown()  event is fired

2)Call Application.DoEvents() to update progress status
