# AgriSmart API Documentation

## Base URL
```
Development: http://localhost:5273
Production: https://your-production-url.com
```

## Authentication

All endpoints (except `/api/auth/*`) require JWT Bearer token authentication.

### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "your_username",
  "password": "your_password"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "your_username",
  "status": 1
}
```

### Register
```http
POST /api/auth/register
Content-Type: application/json

{
  "fullName": "John Doe",
  "username": "johndoe",
  "email": "john@example.com",
  "password": "securepassword",
  "role": "Farmer"
}
```

**Roles:** `Farmer`, `Expert`, `Admin`

### Using the Token
Include the token in the `Authorization` header:
```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## User Management Endpoints

### 1. Get My Profile
Get the profile of the currently authenticated user.

```http
GET /api/v1/users/me
Authorization: Bearer <token>
```

**Response:**
```json
{
  "message": "Profile retrieved successfully",
  "profile": {
    "id": 1,
    "fullName": "John Doe",
    "username": "johndoe",
    "email": "john@example.com",
    "role": "Farmer",
    "createdAt": "2024-01-15T10:00:00Z",
    "cropCount": 3,
    "diagnosisCount": 5
  },
  "status": 1
}
```

---

### 2. Update My Profile
Update the profile of the currently authenticated user.

```http
PUT /api/v1/users/me
Authorization: Bearer <token>
Content-Type: application/json

{
  "fullName": "John Smith",
  "username": "johnsmith",
  "email": "johnsmith@example.com"
}
```

**Response:**
```json
{
  "message": "Profile updated successfully",
  "user": {
    "id": 1,
    "fullName": "John Smith",
    "username": "johnsmith",
    "email": "johnsmith@example.com",
    "role": "Farmer",
    "createdAt": "2024-01-15T10:00:00Z"
  },
  "status": 1
}
```

---

### 3. Change Password
Change the password of the currently authenticated user.

```http
PUT /api/v1/users/me/password
Authorization: Bearer <token>
Content-Type: application/json

{
  "currentPassword": "oldpassword",
  "newPassword": "newpassword123",
  "confirmPassword": "newpassword123"
}
```

**Response:**
```json
{
  "message": "Password changed successfully",
  "status": 1
}
```

---

### 4. Get All Users (Admin Only)
Retrieve all users in the system.

```http
GET /api/v1/users
Authorization: Bearer <token> (Admin role required)
```

**Response:**
```json
{
  "message": "Users retrieved successfully",
  "users": [
    {
      "id": 1,
      "fullName": "John Doe",
      "username": "johndoe",
      "email": "john@example.com",
      "role": "Farmer",
      "createdAt": "2024-01-15T10:00:00Z"
    }
  ],
  "status": 1
}
```

---

### 5. Get User By ID (Admin Only)
Retrieve a specific user by their ID.

```http
GET /api/v1/users/1
Authorization: Bearer <token> (Admin role required)
```

---

### 6. Get User By Username (Admin Only)
Retrieve a specific user by their username.

```http
GET /api/v1/users/username/johndoe
Authorization: Bearer <token> (Admin role required)
```

---

### 7. Update User (Admin Only)
Update any user's profile by ID.

```http
PUT /api/v1/users/1
Authorization: Bearer <token> (Admin role required)
Content-Type: application/json

{
  "fullName": "Updated Name",
  "role": "Admin"
}
```

---

### 8. Delete User (Admin Only)
Delete a user by ID.

```http
DELETE /api/v1/users/1
Authorization: Bearer <token> (Admin role required)
```

**Response:**
```json
{
  "message": "User deleted successfully",
  "status": 1
}
```

---

## Crop Management Endpoints

### 9. Generate Crop Calendar
Generate a crop planting and harvest schedule.

```http
POST /api/v1/crop/calender
Authorization: Bearer <token>
Content-Type: application/json

{
  "cropName": "Tomato",
  "location": "California",
  "plantingDate": "2024-03-15"
}
```

**Response:**
```json
{
  "message": "Crop calendar generated successfully",
  "cropId": 1,
  "location": "California",
  "cropName": "Tomato",
  "plantingDate": "2024-03-15T00:00:00",
  "careSchedule": "Water every 2 days, fertilize on 2024-03-25, Maintain moderate moisture",
  "harvestSchedule": "Harvest expected on 2024-06-13",
  "status": 1
}
```

---

### 10. Pest Detection
Upload an image to detect pests or diseases.

```http
POST /api/v1/crop/pests/detector
Authorization: Bearer <token>
Content-Type: multipart/form-data

Image: <file>
```

**Response:**
```json
{
  "message": "Pest diagnosis completed",
  "diagnosisId": 1,
  "imageUrl": "plant_image.jpg",
  "diagnosis": "Leaf Blight",
  "treatmentRecommendation": "Apply fungicide immediately. Remove infected leaves.",
  "confidence": 0.95
}
```

---

### 11. Soil Type Prediction
Upload soil image to predict soil type and get crop recommendations.

```http
POST /api/v1/soil/type/predict
Authorization: Bearer <token>
Content-Type: multipart/form-data

Image: <file>
```

**Response:**
```json
{
  "id": 1,
  "soilType": "Clay",
  "description": "Rich in nutrients, slow drainage",
  "recommendedCrops": "Rice, Wheat, Cotton",
  "predictionDate": "2024-03-15T10:30:00Z"
}
```

---

### 12. Get User Crops
Retrieve all crops for the authenticated user.

```http
GET /api/v1/crops/user
Authorization: Bearer <token>
```

**Response:**
```json
{
  "message": "Crops retrieved successfully",
  "crops": [
    {
      "cropId": 1,
      "cropName": "Tomato",
      "location": "California",
      "plantingDate": "2024-03-15T00:00:00",
      "careSchedule": "Water every 2 days...",
      "harvestSchedule": "Harvest expected on 2024-06-13",
      "createdAt": "2024-03-15T10:30:00Z"
    }
  ],
  "status": 1
}
```

---

## API Versioning

The API supports versioning via URL path or header:

- **URL Path:** `/api/v1/...`
- **Header:** `X-Api-Version: 1.0`

---

## Health Check

Check API status:

```http
GET /health
```

**Response:**
```json
{
  "Status": "Healthy",
  "Timestamp": "2024-03-15T10:30:00Z"
}
```

---

## Swagger Documentation

Access interactive API docs at: `http://localhost:5273/swagger`

---

## Error Responses

| Status Code | Meaning |
|-------------|---------|
| 400 | Bad Request - Invalid input |
| 401 | Unauthorized - Invalid or missing token |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Resource not found |
| 500 | Internal Server Error |

**Error Format:**
```json
{
  "statusCode": 500,
  "message": "Error description",
  "detail": "Additional details"
}
```

---

## CORS

Allowed origins (configure in `appsettings.json`):
- Development: `http://localhost:3000`, `http://localhost:5273`
- Production: Configure specific domains

---

## Rate Limiting

Available in production (disabled in development):
- 100 requests per hour per IP

---

## Environment Variables (Production)

| Variable | Description |
|----------|-------------|
| `DB_HOST` | PostgreSQL host |
| `DB_NAME` | Database name |
| `DB_USER` | Database username |
| `DB_PASSWORD` | Database password |
| `JWT_KEY` | Secret key for JWT signing |
| `JWT_ISSUER` | JWT issuer |
| `JWT_AUDIENCE` | JWT audience |
| `CORS_ORIGINS` | Allowed origins (comma-separated) |
| `FLASK_API_URL` | External Flask API URL |
| `WEATHER_API_KEY` | Weather API key |