Info:
1. The application supports several simulated tickers.
2. The application supports register, login and ticker dashboards displaying the triggers
3. The application supports selecting triggers for user and stroring the selection in db. On next load the selected triggers will be loaded automatically
4. The application is based on clean code architecture
5. The application is developed with .Net as a backend and React as a frontend. React was chosen as the most popular frontend library
6. Backend uses Entity Framework for database access. EF was chosen as it allows easy maintenance and is understood by most developers. 
7. The application uses mediatr and fluent validation to provide separation of concerns. 


How to run:
requirements: docker and docker compose
1. Copy the compose.yaml file locally
2. run docker compose up
3. Go to http://localhost:3000
4. register a user
5. login with the new registered user
6. go to dashboard and play around with the triggers
7. Ticker time and supported tickers can be configured in docker compose or in settings file if running outside of docker

Not implemented:
- Tickers drag and drop, color schemes, customization - these need to be implemented in the same way, the ticker selection is implemented. On every change the backend needs to be notified and the data will be stored
- React app stores JWT token in local storage. This is done only for demonstration purposes. This needs to be update to store the token in an appropriate storage such as HTTP-only secure cookie
- React app doesn't support refresh. This is not due to the app but to nginx server in docker. The server configuration is out of scope, so it was left as is

TDD:
Tdd principles were followed by first creating the structure and needed files, but leaving the implementation empty. Next step was to create the tests which fail on run. After that the implementation was added and the tests were run again. issues were fixed until all tests were passing 
