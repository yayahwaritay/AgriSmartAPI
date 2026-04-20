# AgriSmart Sierra API Documentation

A comprehensive API for agricultural farming management with support for farm management, crops tracking, marketplace, ML predictions, weather integration, and more.

## Base URL

```
http://localhost:5000/api
```

---

## API Endpoints Flow

The following diagram shows how endpoints flow based on their relationships:

```
┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
│                                    API ENDPOINT FLOW                                             │
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────┘

1. AUTHENTICATION (Start Here)
   └── POST /api/auth/register      → Creates new user account
       └── POST /api/auth/login     → Returns JWT token (use for all authenticated endpoints)

2. USER PROFILE MANAGEMENT
   ├── GET /api/users/{id}          → Get user details
   ├── PUT /api/users/{id}          → Update user details
   ├── GET /api/users/profile/farmer → Get farmer profile
   ├── POST /api/users/profile/farmer → Create farmer profile
   ├── PUT /api/users/profile/farmer/{id} → Update farmer profile
   ├── GET /api/users/profile/buyer → Get buyer profile
   ├── POST /api/users/profile/buyer → Create buyer profile
   ├── GET /api/users/profile/agronomist → Get agronomist profile
   ├── POST /api/users/profile/agronomist → Create agronomist profile
   └── GET /api/users/agronomists    → Get verified agronomists

3. FARM MANAGEMENT
   ├── GET /api/farms/{id}          → Get farm details
   ├── GET /api/farms/farmer/{farmerProfileId} → Get farms by farmer
   ├── POST /api/farms              → Create new farm
   ├── PUT /api/farms/{id}           → Update farm details
   └── DELETE /api/farms/{id}        → Delete farm

4. CROP MANAGEMENT
   ├── GET /api/crops/{id}          → Get crop details
   ├── GET /api/crops/farm/{farmId}  → Get crops by farm
   ├── POST /api/crops              → Create new crop
   ├── PUT /api/crops/{id}         → Update crop
   ├── DELETE /api/crops/{id}       → Delete crop
   └── GET /api/crops/{id}/calendar → Get crop calendar

5. CROP ACTIVITIES
   ├── GET /api/crops/activity/upcoming → Get upcoming activities
   ├── POST /api/crops/activity    → Create activity
   └── PUT /api/crops/activity/{id}/complete → Mark activity complete

6. WEATHER (Independent - No Auth Required)
   ├── GET /api/weather/current    → Get current weather
   ├── GET /api/weather/forecast  → Get weather forecast
   └── GET /api/weather/alerts    → Get weather alerts

7. RESOURCES CALCULATOR
   ├── POST /api/resources/fertilizer → Calculate fertilizer needs
   ├── POST /api/resources/water      → Calculate water needs
   └── GET /api/resources/size       → Calculate farm size

8. MACHINE LEARNING PREDICTIONS
   ├── POST /api/ml/predict-disease  → Predict crop disease (image upload)
   ├── GET /api/ml/predict-yield/{cropId} → Predict yield
   └── POST /api/ml/weather-recommendations → Weather recommendations

9. PEST MANAGEMENT
   ├── GET /api/pests/{id}           → Get pest report
   ├── GET /api/pests/crop/{cropId} → Get pest reports by crop
   ├── GET /api/pests/pending       → Get pending pest reports
   ├── POST /api/pests               → Create pest report
   └── PUT /api/pests/{id}/status   → Update pest report status

10. MARKETPLACE
    ├── GET /api/marketplace/{id}   → Get listing details
    ├── GET /api/marketplace       → Get active listings
    ├── POST /api/marketplace       → Create listing
    ├── PUT /api/marketplace/{id}   → Update listing
    ├── DELETE /api/marketplace/{id} → Delete listing
    └── GET /api/marketplace/my-listings → Get my listings

11. ORDERS
    ├── GET /api/orders/{id}        → Get order details
    ├── GET /api/orders/buyer       → Get buyer's orders
    ├── GET /api/orders/seller      → Get seller's orders
    ├── POST /api/orders           → Create order
    └── PUT /api/orders/{id}/status → Update order status

12. PRICES (Public)
    ├── GET /api/prices             → Get all crop prices
    └── GET /api/prices/{cropName} → Get price by crop

13. FORUM
    ├── GET /api/forum/post/{id}   → Get post details
    ├── GET /api/forum/posts/recent → Get recent posts
    ├── GET /api/forum/posts/category/{category} → Get posts by category
    ├── POST /api/forum/post       → Create post
    ├── PUT /api/forum/post/{id}   → Update post
    ├── DELETE /api/forum/post/{id} → Delete post
    ├── GET /api/forum/comment/{id} → Get comment
    ├── GET /api/forum/post/{postId}/comments → Get comments by post
    ├── POST /api/forum/comment    → Create comment
    └── POST /api/forum/comment/{commentId}/upvote → Upvote comment

14. FINANCE - LOANS
    ├── GET /api/finance/loan/{id}  → Get loan details
    ├── GET /api/finance/loans/my  → Get my loans
    ├── POST /api/finance/loan     → Apply for loan
    └── PUT /api/finance/loan/{id} → Update loan

15. FINANCE - INSURANCE
    ├── GET /api/finance/insurance/{id} → Get insurance
    ├── GET /api/finance/insurances/my → Get my insurances
    ├── POST /api/finance/insurance → Create insurance
    └── GET /api/finance/insurances/active → Get active insurances (public)

16. ANALYTICS
    ├── GET /api/analytics/dashboard → Get dashboard
    ├── GET /api/analytics/input-usage/{farmId} → Get input usage
    └── GET /api/analytics/profit-estimate/{cropId} → Estimate profit
```

---

## Enums Reference

This section lists all enum values used throughout the API.

### UserRole
Used in: `/api/auth/register`
```
Farmer, Buyer, Agronomist, Admin
```

### CropCategory
Used in: `/api/crops` (CreateCropDto)
```
Cereal, Grain, Vegetable, Fruit, Root, Legume, CashCrop
```

### CropStatus
Used in: `/api/crops` (CreateCropDto, UpdateCropDto)
```
Planned, Planted, Growing, Flowering, Fruiting, Harvested, Failed
```

### ActivityType
Used in: `/api/crops/activity` (CreateCropActivityDto)
```
Planting, Watering, Fertilizing, PestControl, Weeding, Pruning, Harvesting, SoilPreparation, Irrigation, Inspection
```

### ActivityStatus
Used in: `/api/crops/activity` (CompleteActivity)
```
Scheduled, InProgress, Completed, Cancelled, Delayed
```

### ListingStatus
Used in: `/api/marketplace` (UpdateMarketplaceListingDto)
```
Active, Pending, Sold, Expired, Cancelled
```

### OrderStatus
Used in: `/api/orders` (UpdateOrderStatusDto)
```
Pending, Confirmed, Processing, Shipped, Delivered, Cancelled, Refunded
```

### LoanStatus
Used in: `/api/finance/loan` (UpdateLoanApplicationDto)
```
Pending, UnderReview, Approved, Rejected, Disbursed, Repaid, Defaulted
```

### ReportStatus
Used in: `/api/pests` (Update Pest Status)
```
Pending, Analyzed, Treated, Resolved, Failed
```

### WeatherAlertType
Used in: `/api/weather/alerts`
```
None, Flood, Drought, Storm, HeatWave, ColdWave
```

---

## Detailed Endpoint Documentation

### 1. Authentication

#### POST /api/auth/register
Register a new user account.

**Request:**
```json
{
  "email": "farmer@agrismart.com",
  "password": "SecurePass123!",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+254712345678",
  "role": "Farmer"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Registration successful",
  "data": {
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "email": "farmer@agrismart.com",
    "firstName": "John",
    "lastName": "Doe",
    "role": "Farmer",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

---

#### POST /api/auth/login
Login with existing credentials.

**Request:**
```json
{
  "email": "farmer@agrismart.com",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Login successful",
  "data": {
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "email": "farmer@agrismart.com",
    "firstName": "John",
    "lastName": "Doe",
    "role": "Farmer",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

---

### 2. User Management

All user endpoints require JWT token in header: `Authorization: Bearer <token>`

#### GET /api/users/{id}
Get user details by ID.

**Response:**
```json
{
  "success": true,
  "message": "User retrieved successfully",
  "data": {
    "id": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "email": "farmer@agrismart.com",
    "firstName": "John",
    "lastName": "Doe",
    "phoneNumber": "+254712345678",
    "role": "Farmer",
    "profileImageUrl": "https://storage.agrismart.com/profiles/user.jpg",
    "isActive": true,
    "createdAt": "2024-01-15T10:00:00Z"
  }
}
```

---

#### PUT /api/users/{id}
Update user details.

**Request:**
```json
{
  "firstName": "John",
  "lastName": "Smith",
  "phoneNumber": "+254798765432",
  "profileImageUrl": "https://storage.agrismart.com/profiles/new.jpg"
}
```

**Response:**
```json
{
  "success": true,
  "message": "User updated successfully",
  "data": {
    "id": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "email": "farmer@agrismart.com",
    "firstName": "John",
    "lastName": "Smith",
    "phoneNumber": "+254798765432",
    "role": "Farmer",
    "isActive": true,
    "createdAt": "2024-01-15T10:00:00Z"
  }
}
```

---

#### POST /api/users/profile/farmer
Create farmer profile (requires authentication).

**Request:**
```json
{
  "farmName": "Green Valley Farm",
  "farmSizeHectares": 50.5,
  "location": "Nairobi, Kenya",
  "latitude": -1.2921,
  "longitude": 36.8219,
  "district": "Kiambu",
  "soilType": "Loamy",
  "waterSource": "River",
  "irrigationType": "Drip"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Farmer profile created successfully",
  "data": {
    "id": "b2c3d4e5-f6a7-4890-b123-456789012345",
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "farmName": "Green Valley Farm",
    "farmSizeHectares": 50.5,
    "location": "Nairobi, Kenya",
    "latitude": -1.2921,
    "longitude": 36.8219,
    "district": "Kiambu",
    "soilType": "Loamy",
    "waterSource": "River",
    "irrigationType": "Drip",
    "createdAt": "2024-01-20T08:30:00Z"
  }
}
```

---

#### GET /api/users/profile/farmer
Get authenticated user's farmer profile.

**Response:**
```json
{
  "success": true,
  "message": "Farmer profile retrieved",
  "data": {
    "id": "b2c3d4e5-f6a7-4890-b123-456789012345",
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "farmName": "Green Valley Farm",
    "farmSizeHectares": 50.5,
    "location": "Nairobi, Kenya",
    "latitude": -1.2921,
    "longitude": 36.8219,
    "district": "Kiambu",
    "soilType": "Loamy",
    "waterSource": "River",
    "irrigationType": "Drip",
    "createdAt": "2024-01-20T08:30:00Z"
  }
}
```

---

#### POST /api/users/profile/buyer
Create buyer profile.

**Request:**
```json
{
  "companyName": "Fresh Produce Ltd",
  "businessType": "Wholesale",
  "address": "P.O. Box 12345, Nairobi",
  "taxId": "T123456789A"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Buyer profile created successfully",
  "data": {
    "id": "c3d4e5f6-a7b8-4901-c234-567890123456",
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "companyName": "Fresh Produce Ltd",
    "businessType": "Wholesale",
    "address": "P.O. Box 12345, Nairobi",
    "taxId": "T123456789A",
    "createdAt": "2024-01-21T09:00:00Z"
  }
}
```

---

#### POST /api/users/profile/agronomist
Create agronomist profile.

**Request:**
```json
{
  "qualification": "PhD in Agricultural Science",
  "specialization": "Crop Science",
  "yearsOfExperience": 10,
  "serviceArea": "Central Kenya"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Agronomist profile created successfully",
  "data": {
    "id": "d4e5f6a7-b8c9-4012-d345-678901234567",
    "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "firstName": "Jane",
    "lastName": "Doe",
    "qualification": "PhD in Agricultural Science",
    "specialization": "Crop Science",
    "yearsOfExperience": 10,
    "serviceArea": "Central Kenya",
    "isVerified": false,
    "createdAt": "2024-01-22T10:00:00Z"
  }
}
```

---

#### GET /api/users/agronomists/verified
Get all verified agronomists (public endpoint).

**Response:**
```json
{
  "success": true,
  "message": "Verified agronomists retrieved",
  "data": [
    {
      "id": "d4e5f6a7-b8c9-4012-d345-678901234567",
      "userId": "a1b2c3d4-e5f6-4789-a012-345678901234",
      "firstName": "Jane",
      "lastName": "Doe",
      "qualification": "PhD in Agricultural Science",
      "specialization": "Crop Science",
      "yearsOfExperience": 10,
      "serviceArea": "Central Kenya",
      "isVerified": true,
      "createdAt": "2024-01-22T10:00:00Z"
    }
  ]
}
```

---

### 3. Farm Management

#### POST /api/farms
Create a new farm (requires farmer profile).

**Request:**
```json
{
  "name": "Green Valley Main Farm",
  "size": 50.5,
  "location": "Kiambu, Kenya",
  "latitude": -1.2921,
  "longitude": 36.8219,
  "soilType": "Loamy",
  "waterSource": "River",
  "irrigationType": "Drip"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Farm created successfully",
  "data": {
    "id": "e5f6a7b8-c9d0-4123-e456-789012345678",
    "farmerProfileId": "b2c3d4e5-f6a7-4890-b123-456789012345",
    "name": "Green Valley Main Farm",
    "size": 50.5,
    "location": "Kiambu, Kenya",
    "latitude": -1.2921,
    "longitude": 36.8219,
    "soilType": "Loamy",
    "waterSource": "River",
    "irrigationType": "Drip",
    "createdAt": "2024-01-25T11:00:00Z"
  }
}
```

---

#### GET /api/farms/farmer/{farmerProfileId}
Get all farms by farmer profile ID.

**Response:**
```json
{
  "success": true,
  "message": "Farms retrieved successfully",
  "data": [
    {
      "id": "e5f6a7b8-c9d0-4123-e456-789012345678",
      "farmerProfileId": "b2c3d4e5-f6a7-4890-b123-456789012345",
      "name": "Green Valley Main Farm",
      "size": 50.5,
      "location": "Kiambu, Kenya",
      "latitude": -1.2921,
      "longitude": 36.8219,
      "soilType": "Loamy",
      "waterSource": "River",
      "irrigationType": "Drip",
      "createdAt": "2024-01-25T11:00:00Z"
    }
  ]
}
```

---

### 4. Crop Management

#### POST /api/crops
Create a new crop.

**Request:**
```json
{
  "name": "Maize",
  "variety": "Hybrid DH-512",
  "farmId": "e5f6a7b8-c9d0-4123-e456-789012345678",
  "plantingDate": "2024-03-01",
  "expectedHarvestDate": "2024-07-01",
  "areaPlanted": 30.0,
  "quantityPlanted": 30,
  "unit": "kg",
  "status": "Planted"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Crop created successfully",
  "data": {
    "id": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "name": "Maize",
    "variety": "Hybrid DH-512",
    "farmId": "e5f6a7b8-c9d0-4123-e456-789012345678",
    "plantingDate": "2024-03-01",
    "expectedHarvestDate": "2024-07-01",
    "areaPlanted": 30.0,
    "quantityPlanted": 30,
    "unit": "kg",
    "status": "Planted",
    "createdAt": "2024-03-01T08:00:00Z"
  }
}
```

---

#### GET /api/crops/farm/{farmId}
Get all crops for a specific farm.

**Response:**
```json
{
  "success": true,
  "message": "Crops retrieved successfully",
  "data": [
    {
      "id": "f6a7b8c9-d0e1-4234-f567-890123456789",
      "name": "Maize",
      "variety": "Hybrid DH-512",
      "farmId": "e5f6a7b8-c9d0-4123-e456-789012345678",
      "plantingDate": "2024-03-01",
      "expectedHarvestDate": "2024-07-01",
      "areaPlanted": 30.0,
      "quantityPlanted": 30,
      "unit": "kg",
      "status": "Planted",
      "createdAt": "2024-03-01T08:00:00Z"
    }
  ]
}
```

---

#### GET /api/crops/{id}/calendar
Get crop calendar with activities.

**Response:**
```json
{
  "success": true,
  "message": "Crop calendar retrieved successfully",
  "data": {
    "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "cropName": "Maize",
    "variety": "Hybrid DH-512",
    "activities": [
      {
        "id": "g7h8i9j0-e1f2-4345-g678-901234567890",
        "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
        "activityType": "Fertilizer Application",
        "scheduledDate": "2024-04-15",
        "description": "Apply top-dressing fertilizer",
        "status": "Pending"
      },
      {
        "id": "h8i9j0k1-f2g3-4456-h789-012345678901",
        "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
        "activityType": "Pest Control",
        "scheduledDate": "2024-05-01",
        "description": "Apply pesticide forFall Armyworm",
        "status": "Pending"
      }
    ]
  }
}
```

---

#### POST /api/crops/activity
Create crop activity.

**Request:**
```json
{
  "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
  "type": "Fertilizing",
  "description": "Apply top-dressing fertilizer (CAN)",
  "scheduledDate": "2024-04-15T08:00:00Z",
  "cost": 15000,
  "notes": "Apply CAN fertilizer at 100kg per hectare"
}
```

**Valid Activity Types:**
- `Planting`
- `Watering`
- `Fertilizing`
- `PestControl`
- `Weeding`
- `Pruning`
- `Harvesting`
- `SoilPreparation`
- `Irrigation`
- `Inspection`

**Response:**
```json
{
  "success": true,
  "message": "Activity created successfully",
  "data": {
    "id": "g7h8i9j0-e1f2-4345-g678-901234567890",
    "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "assignedTo": null,
    "type": "Fertilizing",
    "description": "Apply top-dressing fertilizer (CAN)",
    "scheduledDate": "2024-04-15T08:00:00Z",
    "completedDate": null,
    "status": "Scheduled",
    "cost": 15000,
    "notes": "Apply CAN fertilizer at 100kg per hectare",
    "createdAt": "2024-04-01T09:00:00Z"
  }
}
```

---

### 5. Weather

#### GET /api/weather/current
Get current weather (public - no auth required).

**Request Parameters:**
- `latitude` (required): Geographic latitude
- `longitude` (required): Geographic longitude
- `location` (required): list countries to select from

**Response:**
```json
{
  "success": true,
  "message": "",
  "data": {
    "id": "e963bdc4-6e60-4393-8e8f-6e976d19f99b",
    "location": "Freetown, Sierra Leone",
    "temperature": 39.68,
    "humidity": 19,
    "rainfall": 0,
    "windSpeed": 0.44,
    "weatherCondition": "Clear",
    "weatherDescription": null,
    "alertType": "Drought",
    "alertMessage": "Low humidity. Ensure adequate irrigation.",
    "recordedAt": "2026-04-05T16:40:19.0252574Z"
  },
  "errors": null
}
```

---

#### GET /api/weather/forecast
Get weather forecast (public - no auth required).

**Request Parameters:**
- `latitude` (required): Geographic latitude
- `longitude` (required): Geographic longitude
- `days` (optional): Number of days (default: 5, max: 7)

**Response:**
```json
{
  "success": true,
  "message": "Weather forecast retrieved",
  "data": [
    {
      "date": "2024-04-02",
      "temperature": {
        "min": 18,
        "max": 28
      },
      "humidity": 70,
      "condition": "Rain",
      "precipitation": 80,
      "windSpeed": 15,
      "uvIndex": 4
    },
    {
      "date": "2024-04-03",
      "temperature": {
        "min": 19,
        "max": 30
      },
      "humidity": 60,
      "condition": "Sunny",
      "precipitation": 0,
      "windSpeed": 10,
      "uvIndex": 8
    }
  ]
}
```

---

### 6. Resources Calculator

#### POST /api/resources/fertilizer
Calculate fertilizer recommendations.

**Request:**
```json
{
  "cropType": "Maize",
  "soilType": "Loamy",
  "area": 10.0,
  "currentNitrogen": 40,
  "targetNitrogen": 60,
  "fertilizerType": "CAN"
}
```

**Response:**
```json
{
	"success": true,
	"message": "",
	"data": {
		"fertilizerType": "NPK 20-10-10",
		"amountPerHectare": 250,
		"unit": "kg/ha",
		"totalAmount": 0,
		"applicationTiming": "At planting and 6 weeks after",
		"notes": "Apply based on soil test results"
	},
	"errors": null
}
```

---

#### POST /api/resources/water
Calculate water recommendations.

**Request:**
```json
{
  "cropType": "Maize",
  "soilType": "Loamy",
  "area": 10.0,
  "irrigationMethod": "Drip",
  "evapotranspiration": 5.0
}
```

**Response:**
```json
{
	"success": true,
	"message": "",
	"data": {
		"dailyWaterNeedLiters": 0,
		"weeklyWaterNeedLiters": 0,
		"irrigationMethod": "Drip",
		"bestTimesToWater": [
			"Early Morning (6-8 AM)",
			"Late Evening (5-7 PM)"
		],
		"notes": "Adjust based on weather conditions"
	},
	"errors": null
}
```

---

### 7. Machine Learning Predictions

#### POST /api/MLPredictions/predict-disease
Predict crop disease from leaf image.

**Request:**
- `file` (form-data): Image file of leaf
- `cropId` (query): Crop ID

**Response:**
```json
{
	"success": true,
	"message": "",
	"data": {
		"status": "success",
		"result": {
			"confidence": 99.95751976966858,
			"detections": [
				{
					"disease": "Corn___Cercospora_leaf_spot Gray_leaf_spot",
					"confidence": 99.95751976966858,
					"advice": "No specific treatment advice available. Consult a local agronomist."
				}
			],
			"bounded_image": "data:image/png;base64,"	
    }
	},
	"errors": null
}
```

---

#### GET /api/MLPredictions/predict-yield/{cropId}
Predict crop yield.

**Response:**
```json
{
  "success": true,
  "message": "",
  "data": {
    "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
    "cropName": "Maize",
    "predictedYield": 0.000,
    "unit": "kg",
    "confidenceScore": 0.8673887571570063,
    "predictedAt": "2026-04-05T18:26:16.0510325Z",
    "factors": [
      {
        "factorName": "Weather",
        "impact": "Positive",
        "description": "Good rainfall patterns"
      },
      {
        "factorName": "Soil",
        "impact": "Neutral",
        "description": "Adequate nutrients"
      }
    ]
  },
  "errors": null
}
```

---

### 8. Marketplace

#### GET /api/marketplace
Get active marketplace listings (public endpoint).

**Request Parameters:**
- `searchTerm` (optional): Search term
- `category` (optional): Category filter

**Response:**
```json
{
  "success": true,
  "message": "",
  "data": [
    {
      "id": "98a7de68-905b-4310-b3db-afe6f9e21fc5",
      "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
      "sellerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
      "title": "Fresh Cucumba - Grade A",
      "description": "High quality maize ready for sale",
      "pricePerUnit": 0.00,
      "unit": "ton",
      "availableQuantity": 0.00,
      "minimumOrderQuantity": null,
      "imageUrl": null,
      "status": "Active",
      "qualityGrade": null,
      "isOrganic": false,
      "cropName": "",
      "sellerName": "",
      "createdAt": "2026-04-09T19:50:08.3848312"
    }
  ],
  "errors": null
}
```

---

#### POST /api/marketplace
Create marketplace listing.

**Request (multipart/form-data):**
- `title`: "Fresh Maize - Grade A"
- `cropId`: "f1c2fc69-e012-486e-b207-c404b5486681"
- `description`: "High quality maize ready for sale"
- `QualityGrade`: "A"
- `PricePerUnit`: 250
- `currency`: "KES"
- `unit`: "ton"
- `AvailableQuantity`: 50
- `MinimumOrderQuantity`: 20
- `image`: (file)
- `IsOrganic`: true/false

**Response:**
```json
{
  "success": true,
  "message": "Listing created",
  "data": {
    "id": "98a7de68-905b-4310-b3db-afe6f9e21fc5",
    "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
    "sellerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
    "title": "Fresh Cucumba - Grade A",
    "description": "High quality maize ready for sale",
    "pricePerUnit": 250,
    "unit": "ton",
    "availableQuantity": 50,
    "minimumOrderQuantity": 20,
    "imageUrl": null,
    "status": "Active",
    "qualityGrade": null,
    "isOrganic": false,
    "cropName": "",
    "sellerName": "",
    "createdAt": "2026-04-09T19:50:08.3848312Z"
  },
  "errors": null
}
```

---
#### POST /api/marketplace
update marketplace listing.

**Request Parameters:**
- `id` (required): "26fc0628-6875-4633-b19d-928b726ae751"

**Request (multipart/form-data):**
- `title`: "Fresh Maize - Grade A"
- `cropId`: "f1c2fc69-e012-486e-b207-c404b5486681"
- `description`: "High quality maize ready for sale"
- `PricePerUnit`: 2500
- `AvailableQuantity`: 50
- `Status`: "Active"

**Response:**
```json
{
  "success": true,
  "message": "Listing created",
  "data": {
    "id": "90ecb9bd-b28a-4ea5-8fdd-d05c925fe49e",
    "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
    "sellerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
    "title": "Fresh Maize - Grade A",
    "description": "",
    "pricePerUnit": 20,
    "unit": "kg",
    "availableQuantity": 10,
    "minimumOrderQuantity": null,
    "imageUrl": null,
    "status": "Active",
    "qualityGrade": null,
    "isOrganic": false,
    "cropName": "",
    "sellerName": "",
    "createdAt": "2026-04-09T20:48:04.9175607Z"
  },
  "errors": null
}
```

---

#### GET /api/marketplace/my-listings
Get marketplace listing.

**Response:**
```json
{
  "success": true,
  "message": "",
  "data": [
    {
      "id": "26fc0628-6875-4633-b19d-928b726ae751",
      "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
      "sellerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
      "title": "Fresh Maize - Grade A",
      "description": "High quality maize ready for sale",
      "pricePerUnit": 0.00,
      "unit": "ton",
      "availableQuantity": 0.00,
      "minimumOrderQuantity": null,
      "imageUrl": null,
      "status": "Active",
      "qualityGrade": null,
      "isOrganic": false,
      "cropName": "",
      "sellerName": "",
      "createdAt": "2026-04-09T19:49:20.5163195"
    },
    {
      "id": "98a7de68-905b-4310-b3db-afe6f9e21fc5",
      "cropId": "f1c2fc69-e012-486e-b207-c404b5486681",
      "sellerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
      "title": "Fresh Cucumba - Grade A",
      "description": "High quality maize ready for sale",
      "pricePerUnit": 0.00,
      "unit": "ton",
      "availableQuantity": 0.00,
      "minimumOrderQuantity": null,
      "imageUrl": null,
      "status": "Active",
      "qualityGrade": null,
      "isOrganic": false,
      "cropName": "",
      "sellerName": "",
      "createdAt": "2026-04-09T19:50:08.3848312"
    }
  ],
  "errors": null
}
```

---

### 9. Orders

#### POST /api/orders
Create new order.

**Request:**
```json
{
  "listingId": "90ecb9bd-b28a-4ea5-8fdd-d05c925fe49e",
  "quantity": 2,
  "deliveryAddress": "P.O. Box 123, Nairobi",
  "deliveryNotes": "Please deliver on weekdays only"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Order created",
  "data": {
    "id": "ae0a962f-19d1-4118-a735-91c4450a745b",
    "listingId": "90ecb9bd-b28a-4ea5-8fdd-d05c925fe49e",
    "buyerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
    "orderNumber": "ORD-20260409-D5EC1F2C",
    "quantity": 2,
    "unitPrice": 20.00,
    "totalAmount": 40.00,
    "status": "Pending",
    "deliveryAddress": "P.O. Box 123, Nairobi",
    "deliveryNotes": "Please deliver on weekdays only",
    "orderedAt": "2026-04-09T21:05:12.8017688Z",
    "confirmedAt": null,
    "shippedAt": null,
    "deliveredAt": null,
    "createdAt": "2026-04-09T21:05:12.8019617Z"
  },
  "errors": null
}
```

---

#### PUT /api/orders/{id}/status
Update order status.

**Request Parameters:**
- `id` (required): "26fc0628-6875-4633-b19d-928b726ae751"

**Request:**
```json
{
  "status": "Confirmed"
}
```

**Response:**
```json
{
  "success": true,
  "message": "",
  "data": {
    "id": "ae0a962f-19d1-4118-a735-91c4450a745b",
    "listingId": "90ecb9bd-b28a-4ea5-8fdd-d05c925fe49e",
    "buyerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
    "orderNumber": "ORD-20260409-D5EC1F2C",
    "quantity": 2.00,
    "unitPrice": 20.00,
    "totalAmount": 40.00,
    "status": "Confirmed",
    "deliveryAddress": "P.O. Box 123, Nairobi",
    "deliveryNotes": "Please deliver on weekdays only",
    "orderedAt": "2026-04-09T21:05:12.8017688",
    "confirmedAt": "2026-04-09T21:12:17.3225546Z",
    "shippedAt": null,
    "deliveredAt": null,
    "createdAt": "2026-04-09T21:05:12.8019617"
  },
  "errors": null
}
```

---

### 10. Forum

#### POST /api/forum/post
Create forum post.

**Request:**
```json
{
  "title": "Best practices for maize farming in dry season",
  "content": "I would like to share some tips...",
  "category": "Farming Tips"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Post created successfully",
  "data": {
    "id": "k1l2m3n4-h5i6-6789-k012-345678901234",
    "title": "Best practices for maize farming in dry season",
    "content": "I would like to share some tips...",
    "category": "Farming Tips",
    "authorId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "commentsCount": 0,
    "upvotesCount": 0,
    "createdAt": "2024-04-03T09:00:00Z"
  }
}
```

---

#### POST /api/forum/comment
Create comment on post.

**Request:**
```json
{
  "postId": "k1l2m3n4-h5i6-6789-k012-345678901234",
  "content": "Great tips! I will try these."
}
```

**Response:**
```json
{
  "success": true,
  "message": "Comment created successfully",
  "data": {
    "id": "l2m3n4o5-i6j7-7890-l123-456789012345",
    "postId": "k1l2m3n4-h5i6-6789-k012-345678901234",
    "content": "Great tips! I will try these.",
    "authorId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "upvotesCount": 0,
    "createdAt": "2024-04-03T10:00:00Z"
  }
}
```

---

### 11. Pest Reports

#### POST /api/pests
Submit pest report (with image).

**Request (multipart/form-data):**
- `cropId`: "f6a7b8c9-d0e1-4234-f567-890123456789"
- `description`: "this is the description"
- `image`: (file)

**Response:**
```json
{
  "success": true,
  "message": "Report created",
  "data": {
    "id": "m3n4o5p6-j7k8-8901-m234-567890123456",
    "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "reportedById": "6a2fb3b5-d68a-4647-b0a2-4787b4f704cb",
    "imageUrl": "",
    "description": "this is the description",
    "detectedDisease": null,
    "confidenceScore": null,
    "treatmentSuggestions": null,
    "severity": null,
    "status": "Pending",
    "createdAt": "2026-04-12T10:50:21.8743513Z"
  },
  "errors": null
}
```

---

### 12. Finance - Loans

#### POST /api/finance/loan
Apply for agricultural loan.

**Request:**
```json
{
  "lenderName": "oSMAN cONTEH",
  "amountRequested": 1000,
  "purpose": "Equipment Finance",
  "repaymentPeriodMonths": 6
}

```

**Response:**
```json
{
  "success": true,
  "message": "Loan application submitted",
  "data": {
    "id": "ffaf472d-7a2d-47aa-a1ed-ff4a3a2cd095",
    "farmerId": "b98147da-5ffb-4c27-ae4f-54a250b95390",
    "lenderName": "oSMAN cONTEH",
    "amountRequested": 1000,
    "amountApproved": null,
    "purpose": "Equipment Finance",
    "repaymentPeriodMonths": 6,
    "status": "Pending",
    "remarks": null,
    "appliedAt": "2026-04-16T15:18:36.3442571Z"
  },
  "errors": null
}
```

---

### 13. Finance - Insurance

#### POST /api/finance/insurance
Apply for crop insurance.

**Request:**
```json
{
  "insuranceType": "Crop Insurance",
  "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
  "coverageAmount": 1000000,
  "currency": "KES",
  "premium": 25000,
  "startDate": "2024-04-01",
  "endDate": "2024-12-31"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Insurance created successfully",
  "data": {
    "id": "o5p6q7r8-l9m0-0123-o456-789012345678",
    "insuranceType": "Crop Insurance",
    "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "coverageAmount": 1000000,
    "currency": "KES",
    "premium": 25000,
    "startDate": "2024-04-01",
    "endDate": "2024-12-31",
    "status": "Active",
    "farmerId": "a1b2c3d4-e5f6-4789-a012-345678901234",
    "createdAt": "2024-04-05T10:00:00Z"
  }
}
```

---

### 14. Analytics

#### GET /api/analytics/dashboard
Get analytics dashboard.

**Response:**
```json
{
  "success": true,
  "message": "Analytics dashboard retrieved",
  "data": {
    "totalFarms": 2,
    "totalCrops": 5,
    "activeListings": 3,
    "totalOrders": 15,
    "pendingOrders": 5,
    "completedOrders": 10,
    "revenue": 2500000,
    "currency": "KES",
    "upcomingActivities": 8,
    "recentActivity": [
      {
        "type": "Crop Activity",
        "description": "Fertilizer application",
        "status": "Completed",
        "date": "2024-04-01T08:00:00Z"
      }
    ]
  }
}
```

---

#### GET /api/analytics/profit-estimate/{cropId}
Estimate profit for a crop.

**Response:**
```json
{
  "success": true,
  "message": "Profit estimation retrieved",
  "data": {
    "cropId": "f6a7b8c9-d0e1-4234-f567-890123456789",
    "cropName": "Maize",
    "expectedYield": 8.5,
    "unit": "tons/ha",
    "expectedRevenue": 425000,
    "currency": "KES",
    "estimatedCosts": {
      "seeds": 30000,
      "fertilizer": 45000,
      "labor": 50000,
      "irrigation": 20000,
      "pesticides": 25000,
      "total": 170000
    },
    "estimatedProfit": 255000,
    "profitMargin": 60,
    "currency": "KES"
  }
}
```

---

### 15. Prices (Public)

#### GET /api/prices
Get current market prices for crops.

**Response:**
```json
{
  "success": true,
  "message": "Prices retrieved successfully",
  "data": [
    {
      "cropName": "Maize",
      "currentPrice": 25000,
      "previousPrice": 24000,
      "change": 4.17,
      "currency": "KES",
      "unit": "ton",
      "lastUpdated": "2024-04-05T08:00:00Z"
    },
    {
      "cropName": "Wheat",
      "currentPrice": 35000,
      "previousPrice": 36000,
      "change": -2.78,
      "currency": "KES",
      "unit": "ton",
      "lastUpdated": "2024-04-05T08:00:00Z"
    }
  ]
}
```

---

## Complete Workflow Example

Here's a complete example showing how endpoints flow from one to the next:

```
1. USER REGISTRATION
   POST /api/auth/register
   → Returns user ID and JWT token

2. CREATE FARMER PROFILE
   POST /api/users/profile/farmer (with JWT token)
   → Returns farmer profile ID

3. CREATE FARM
   POST /api/farms?farmerProfileId={farmerProfileId} (with JWT token)
   → Returns farm ID

4. ADD CROPS
   POST /api/crops (with JWT token)
   → Returns crop ID

5. CHECK WEATHER
   GET /api/weather/current?latitude=-1.2921&longitude=36.8219
   → Returns weather data

6. CALCULATE RESOURCES
   POST /api/resources/fertilizer (with JWT token)
   → Returns fertilizer recommendations

7. CREATE MARKETPLACE LISTING
   POST /api/marketplace (with JWT token)
   → Returns listing ID

8. PLACE ORDER (if buyer)
   POST /api/orders (with JWT token)
   → Returns order ID

9. GET ANALYTICS
   GET /api/analytics/dashboard (with JWT token)
   → Returns dashboard
```

---

## Error Responses

All endpoints may return the following error responses:

**400 Bad Request:**
```json
{
  "success": false,
  "message": "Validation error details",
  "data": null
}
```

**401 Unauthorized:**
```json
{
  "success": false,
  "message": "Invalid or expired token",
  "data": null
}
```

**403 Forbidden:**
```json
{
  "success": false,
  "message": "You do not have permission to perform this action",
  "data": null
}
```

**404 Not Found:**
```json
{
  "success": false,
  "message": "Resource not found",
  "data": null
}
```

**500 Internal Server Error:**
```json
{
  "success": false,
  "message": "An error occurred while processing your request",
  "data": null
}
```

---

## Authentication

Most endpoints require JWT authentication. Include the token in the request header:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Public endpoints (no authentication required):
- POST /api/auth/register
- POST /api/auth/login
- GET /api/weather/current
- GET /api/weather/forecast
- GET /api/weather/alerts
- GET /api/marketplace
- GET /api/marketplace/{id}
- GET /api/prices
- GET /api/prices/{cropName}
- GET /api/forum/posts/recent
- GET /api/forum/posts/category/{category}
- GET /api/forum/post/{id}/comments
- GET /api/forum/comment/{id}
- GET /api/users/agronomists/verified
- GET /api/finance/insurances/active