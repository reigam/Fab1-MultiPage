# FabulousMultiNavPages
A simple example of a cross platform app (ios and android) using fabulous. 

Each page implements a self contained MVU architecujtre, but also has access to a global model. 

When you start the app, you will begin at the start page. By opening new pages, they will be added to the page stash. By clicking on the back button, the last page of the page stash ill be removed. 
Once the start page is selected, the page stash will reset.

The code is based on this discussion:https://github.com/fsprojects/Fabulous/issues/144#issuecomment-491992327
or more specifically on this original code by Thomas Bandt: https://gist.github.com/aspnetde/076b519029c860191f233a7725bdaa47
  
