# DocumentUpload
A rest API for document analysis and upload

Supported APIs:

POST: http://localhost:9000/upload

Request:
```
Content-Disposition: form-data; name="EMAIL"
abcd.efgh@yahoo.com


Content-Disposition: form-data; name="FILE"; filename="HubdocInvoice5.pdf"
Content-Type: application/pdf
```

Response:
```
{
    "id": "e419b903-a6e4-41fe-8037-91b23ac97053"
}
```

Get: http://localhost:9000/document/{Id}

Response: 
```
{
	"InvoiceDate": 2019-07-12T13:22:34.414-04:00,
	"TotalAmount": 118.65,
	"AmountDue": 0.0,
	"Currency": "Unknown",
	"ProcessingStatus": "Paid",
	"Id": "39d1687a-2f38-44bb-b29d-df425419c807",
	"UploadedBy": "abcd.efgh@yahoo.com",
	"UploadTimestamp": "2019-07-12T13:22:34.414-04:00"
}
```

Get List: http://localhost:9000/document

Response:
```
[{
	"InvoiceDate": null,
	"TotalAmount": 118.65,
	"AmountDue": 0.0,
	"Currency": "Unknown",
	"ProcessingStatus": "Paid",
	"Id": "39d1687a-2f38-44bb-b29d-df425419c807",
	"UploadedBy": "abcd.efgh@yahoo.com",
	"UploadTimestamp": "2019-07-12T13:22:34.414-04:00"
}, {
	"InvoiceDate": "2019-03-18T00:00:00-04:00",
	"TotalAmount": 118.65,
	"AmountDue": 0.0,
	"Currency": "CAD",
	"ProcessingStatus": "Paid",
	"Id": "641a9721-3534-4674-9ad4-1dab9274139d",
	"UploadedBy": "abcd.efgh@yahoo.com",
	"UploadTimestamp": "2019-07-12T13:13:18.309-04:00"
}]
```
