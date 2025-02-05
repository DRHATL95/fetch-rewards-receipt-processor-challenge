# Fetch Coding Excercise - Receipt Processor

## .NET 9 Web API Project

## Coding excersise based on: https://github.com/fetch-rewards/receipt-processor-challenge/tree/main
## Files included are the following:
	- api.yml
	- examples/morning-receipt.json
	- examples/simple-receipt.json
### Since data does *not* need to be persisted between runs, I am using the built-in "IMemoryCache"

# Creating Docker Image
- Open a terminal and navigate to the root directory of the project
- Run the following command to create a docker image
```bash
docker build -t fetch-receipt-processor -f ./FetchReceiptProcessor/Dockerfile .
```

# Running Docker Image
- Run the following command to run the docker image
```bash
docker run -p :8080 -p :8081 fetch-receipt-processor
```

# Accessing the API
- *Note* The port number from the output of the previous command
- *Note* The url should be "0.0.0.0" when running locally with Docker
- *Note* Pass in header "Content-Type: application/json" when making requests
- You can access the two routes from the API
  - POST /receipts/process
  - GET /receipts/{id}/points

# Running Tests
- Use whatever API testing software you would like, personally I tested with Postman
- First, use the POST route to get an ID and generate points for the receipt. You should pass in a JSON object in the body with the receipt data
	- Example: POST http://0.0.0.0:8080/receipts/process
	- Example Body: 
	```json
	{
      "retailer": "M&M Corner Market",
      "purchaseDate": "2022-03-20",
      "purchaseTime": "14:33",
      "items": [
        {
          "shortDescription": "Gatorade",
          "price": "2.25"
        },{
          "shortDescription": "Gatorade",
          "price": "2.25"
        },{
          "shortDescription": "Gatorade",
          "price": "2.25"
        },{
          "shortDescription": "Gatorade",
          "price": "2.25"
        }
      ],
      "total": "9.00"
    }
	```
- Second, use the GET route with the ID to get the points for the receipt
	- Example: GET http://0.0.0.0:8080/receipts/1/points