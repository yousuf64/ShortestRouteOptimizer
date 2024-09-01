# Shortest Route Optimizer
This application calculates the shortest path from one location to another within connected nodes using Dijkstra’s Algorithm.

![Screenshot](https://github.com/user-attachments/assets/44116b96-0b08-42de-aad4-7684fc6e56fe)

## Tech Stack
- Vue
- C# (.NET Core 8.0)

## Prerequisites
- Node.js
- npm
- .NET SDK 8.0 or higher

## Project Setup

### Client (Vue.js App)

1. Navigate to the client directory:
    ```sh
    cd ./ShortestRouteOptimizer.App
    ```

2. Install dependencies:
    ```sh
    npm install
    ```

3. Run the development server:
    ```sh
    npm run dev
    ```

4. Build for production:
    ```sh
    npm run build
    ```

5. Lint and fix files:
    ```sh
    npm run lint
    ```
   
6. Format files:
    ```sh
    npm run format
    ```

### Server (.NET Core API)

1. Navigate to the server directory:
    ```sh
    cd ./ShortestRouteOptimizer.Api
    ```

2. Restore dependencies:
    ```sh
    dotnet restore
    ```

3. Build the project:
    ```sh
    dotnet build
    ```

4. Run the server:
    ```sh
    dotnet run
    ```
