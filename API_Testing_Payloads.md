# Workout Program API - Testing Payloads

This document contains example JSON payloads for testing the workout program API endpoints.

## Testing Sequence

1. [Create Program Shell](#1-create-program-shell)
2. [Configure Template Week](#2-configure-template-week) 
3. [Apply Template to All Weeks](#3-apply-template-to-all-weeks)
4. [Start Program](#4-start-program)
5. [Get Workout Programs](#5-get-workout-programs)

---

## 1. Create Program Shell

**Endpoint:** `POST /api/workout-programs`

**Description:** Creates the basic program structure with name, description, and duration.

```json
{
  "name": "Upper/Lower Split Program",
  "description": "A 12-week upper/lower body split focusing on strength and hypertrophy",
  "durationWeeks": 12,
  "isPublic": false
}
```

**Expected Response:**
```json
{
  "programId": "wp_01234567-89ab-cdef-0123-456789abcdef"
}
```

---

## 2. Configure Template Week

**Endpoint:** `POST /api/workout-programs/{programId}/configure-template`

**Description:** Sets up the template week structure with exercises, sets, and reps that will be applied to all weeks.

**Note:** Replace `{programId}` with the actual program ID from step 1.

```json
{
  "workoutProgramId": "wp_01234567-89ab-cdef-0123-456789abcdef",
  "trainingDaysPerWeek": 3,
  "days": [
    {
      "dayNumber": 1,
      "name": "Monday - Upper Body",
      "description": "Chest, back, shoulders, arms",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Barbell Squats + DB curls",
          "exercises": [
            {
              "exerciseId": "e_01234567-89ab-cdef-0123-456789abcdef",
              "orderIndex": 0,
              "targetSets": 2,
              "targetReps": "6-8",
              "targetWeight": 225.0,
              "targetRpe": "7-8",
              "notes": "Focus on depth and control",
              "progressionRule": "Add 5lbs when all sets hit 8 reps"
            },
            {
              "exerciseId": "e_abcdef01-2345-6789-abcd-ef0123456789",
              "orderIndex": 1,
              "targetSets": 2,
              "targetReps": "8-10",
              "targetWeight": 35.0,
              "targetRpe": "8",
              "notes": "Slow negative, squeeze at top",
              "progressionRule": "Add 2.5lbs when hitting 10 reps consistently"
            }
          ]
        },
        {
          "orderIndex": 1,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Dips + DB rows",
          "exercises": [
            {
              "exerciseId": "e_fedcba98-7654-3210-fedc-ba9876543210",
              "orderIndex": 0,
              "targetSets": 2,
              "targetReps": "6-8",
              "targetWeight": null,
              "targetRpe": "7-8",
              "notes": "Bodyweight, add weight if needed",
              "progressionRule": "Add 2.5lbs when hitting 8 reps consistently"
            },
            {
              "exerciseId": "e_11111111-2222-3333-4444-555555555555",
              "orderIndex": 1,
              "targetSets": 2,
              "targetReps": "10-12",
              "targetWeight": 60.0,
              "targetRpe": "8",
              "notes": "Retract shoulder blades",
              "progressionRule": "Add 5lbs when hitting 12 reps"
            }
          ]
        },
        {
          "orderIndex": 2,
          "type": "GiantSet",
          "restSeconds": 120,
          "notes": "Triceps + Lateral raises + Abs circuit",
          "exercises": [
            {
              "exerciseId": "e_66666666-7777-8888-9999-aaaaaaaaaaaa",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "10-15",
              "targetWeight": 80.0,
              "targetRpe": "8-9",
              "notes": "Keep elbows close to body",
              "progressionRule": "Add 5lbs when hitting 15 reps"
            },
            {
              "exerciseId": "e_bbbbbbbb-cccc-dddd-eeee-ffffffffffff",
              "orderIndex": 1,
              "targetSets": 3,
              "targetReps": "10-15",
              "targetWeight": 20.0,
              "targetRpe": "8-9",
              "notes": "Slight forward lean, control the weight",
              "progressionRule": "Add 2.5lbs when form is perfect at 15 reps"
            },
            {
              "exerciseId": "e_cccccccc-dddd-eeee-ffff-000000000000",
              "orderIndex": 2,
              "targetSets": 3,
              "targetReps": "8-10",
              "targetWeight": null,
              "targetRpe": "9",
              "notes": "Slow and controlled movement",
              "progressionRule": "Add 1 rep when hitting 10 consistently"
            }
          ]
        }
      ]
    },
    {
      "dayNumber": 2,
      "name": "Wednesday - Lower Body",
      "description": "Legs, glutes, calves",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Barbell OHP + Hammer curls",
          "exercises": [
            {
              "exerciseId": "e_dddddddd-eeee-ffff-0000-111111111111",
              "orderIndex": 0,
              "targetSets": 2,
              "targetReps": "6-8",
              "targetWeight": 135.0,
              "targetRpe": "7-8",
              "notes": "Strict form, no leg drive",
              "progressionRule": "Add 2.5lbs when hitting 8 reps"
            },
            {
              "exerciseId": "e_eeeeeeee-ffff-0000-1111-222222222222",
              "orderIndex": 1,
              "targetSets": 2,
              "targetReps": "8-10",
              "targetWeight": 40.0,
              "targetRpe": "8",
              "notes": "Neutral grip, control the eccentric",
              "progressionRule": "Add 2.5lbs when hitting 10 reps"
            }
          ]
        },
        {
          "orderIndex": 1,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Neutral grip lat pulldowns + Leg curls",
          "exercises": [
            {
              "exerciseId": "e_ffffffff-0000-1111-2222-333333333333",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "8-12",
              "targetWeight": 150.0,
              "targetRpe": "8",
              "notes": "Pull to upper chest, squeeze lats",
              "progressionRule": "Add 5lbs when hitting 12 reps"
            },
            {
              "exerciseId": "e_00000000-1111-2222-3333-444444444444",
              "orderIndex": 1,
              "targetSets": 3,
              "targetReps": "12-15",
              "targetWeight": 90.0,
              "targetRpe": "8-9",
              "notes": "Slow eccentric, focus on hamstrings",
              "progressionRule": "Add 5lbs when hitting 15 reps"
            }
          ]
        },
        {
          "orderIndex": 2,
          "type": "GiantSet",
          "restSeconds": 120,
          "notes": "Leg extensions + Cable flies + Neck curls",
          "exercises": [
            {
              "exerciseId": "e_11111111-2222-3333-4444-555555555555",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "10-15",
              "targetWeight": 120.0,
              "targetRpe": "8-9",
              "notes": "Full extension, squeeze quads",
              "progressionRule": "Add 5lbs when hitting 15 reps"
            },
            {
              "exerciseId": "e_22222222-3333-4444-5555-666666666666",
              "orderIndex": 1,
              "targetSets": 3,
              "targetReps": "12-15",
              "targetWeight": 25.0,
              "targetRpe": "8-9",
              "notes": "Wide arc, feel the chest stretch",
              "progressionRule": "Add 2.5lbs when form is perfect"
            },
            {
              "exerciseId": "e_33333333-4444-5555-6666-777777777777",
              "orderIndex": 2,
              "targetSets": 3,
              "targetReps": "15-20",
              "targetWeight": 15.0,
              "targetRpe": "8",
              "notes": "Range of motion emphasis",
              "progressionRule": "Add 1 rep when hitting 20"
            }
          ]
        }
      ]
    },
    {
      "dayNumber": 3,
      "name": "Friday - Full Body",
      "description": "Compound movements and accessories",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Romanian Deadlifts + Seated calf raises",
          "exercises": [
            {
              "exerciseId": "e_44444444-5555-6666-7777-888888888888",
              "orderIndex": 0,
              "targetSets": 2,
              "targetReps": "8-10",
              "targetWeight": 185.0,
              "targetRpe": "7-8",
              "notes": "Hip hinge pattern, feel hamstring stretch",
              "progressionRule": "Add 5lbs when hitting 10 reps with perfect form"
            },
            {
              "exerciseId": "e_55555555-6666-7777-8888-999999999999",
              "orderIndex": 1,
              "targetSets": 2,
              "targetReps": "15-20",
              "targetWeight": 90.0,
              "targetRpe": "9",
              "notes": "Full range of motion, pause at top",
              "progressionRule": "Add 5lbs when hitting 20 reps"
            }
          ]
        },
        {
          "orderIndex": 1,
          "type": "Superset",
          "restSeconds": 90,
          "notes": "Ring push-ups + DB preacher curls",
          "exercises": [
            {
              "exerciseId": "e_66666666-7777-8888-9999-aaaaaaaaaaaa",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "10-12",
              "targetWeight": null,
              "targetRpe": "8",
              "notes": "Adjust ring height for difficulty",
              "progressionRule": "Lower rings when hitting 12 reps consistently"
            },
            {
              "exerciseId": "e_77777777-8888-9999-aaaa-bbbbbbbbbbbb",
              "orderIndex": 1,
              "targetSets": 3,
              "targetReps": "8-10",
              "targetWeight": 30.0,
              "targetRpe": "8-9",
              "notes": "Isolate biceps, no momentum",
              "progressionRule": "Add 2.5lbs when hitting 10 reps"
            }
          ]
        },
        {
          "orderIndex": 2,
          "type": "GiantSet",
          "restSeconds": 120,
          "notes": "Leg press + Skull-crushers + Leg raises",
          "exercises": [
            {
              "exerciseId": "e_88888888-9999-aaaa-bbbb-cccccccccccc",
              "orderIndex": 0,
              "targetSets": 2,
              "targetReps": "8-10",
              "targetWeight": 315.0,
              "targetRpe": "8",
              "notes": "Full range, don't lock knees",
              "progressionRule": "Add 20lbs when hitting 10 reps"
            },
            {
              "exerciseId": "e_99999999-aaaa-bbbb-cccc-dddddddddddd",
              "orderIndex": 1,
              "targetSets": 2,
              "targetReps": "10-12",
              "targetWeight": 75.0,
              "targetRpe": "8",
              "notes": "Keep elbows stationary",
              "progressionRule": "Add 2.5lbs when hitting 12 reps"
            },
            {
              "exerciseId": "e_aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
              "orderIndex": 2,
              "targetSets": 2,
              "targetReps": "AMRAP",
              "targetWeight": null,
              "targetRpe": "9-10",
              "notes": "Controlled movement, no swinging",
              "progressionRule": "Add 1 rep to previous AMRAP total"
            }
          ]
        }
      ]
    }
  ]
}
```

**Expected Response:**
```json
{
  "message": "Template configured successfully"
}
```

---

## 3. Apply Template to All Weeks

**Endpoint:** `POST /api/workout-programs/{programId}/apply-template`

**Description:** Applies the template week to all remaining weeks in the program (weeks 2-12 in this example).

**Request Body:** None required

**Expected Response:**
```json
{
  "message": "Template applied successfully",
  "weeksCreated": 11
}
```

---

## 4. Start Program

**Endpoint:** `POST /api/workout-programs/{programId}/start`

**Description:** Creates a user program instance to begin following the workout program.

```json
{
  "workoutProgramId": "wp_01234567-89ab-cdef-0123-456789abcdef",
  "startDate": "2025-01-01",
  "notes": "Starting my first structured program. Goal is to build strength and size."
}
```

**Expected Response:**
```json
{
  "userProgramId": "up_fedcba98-7654-3210-fedc-ba9876543210"
}
```

---

## 5. Get Workout Programs

**Endpoint:** `GET /api/workout-programs`

**Description:** Retrieves a paginated list of available workout programs.

**Query Parameters:**
- `page=1` - Page number
- `pageSize=10` - Number of programs per page
- `q=upper` - Search term (optional)
- `isPublic=false` - Filter by public/private (optional)

**Example URL:**
```
GET /api/workout-programs?page=1&pageSize=10&q=upper&isPublic=false
```

**Expected Response:**
```json
{
  "items": [
    {
      "id": "wp_01234567-89ab-cdef-0123-456789abcdef",
      "name": "Upper/Lower Split Program",
      "description": "A 12-week upper/lower body split focusing on strength and hypertrophy",
      "durationWeeks": 12,
      "isPublic": false,
      "createdByUser": {
        "id": "u_12345678-90ab-cdef-1234-567890abcdef",
        "email": "user@example.com"
      },
      "createdAt": "2025-01-01T10:00:00Z"
    }
  ],
  "totalCount": 1,
  "pageSize": 10,
  "pageNumber": 1,
  "totalPages": 1,
  "hasPreviousPage": false,
  "hasNextPage": false
}
```

---

## Additional Test Cases

### Simple 3-Day Program Example

For a simpler test case, here's a basic 3-day program:

**Create Program:**
```json
{
  "name": "Beginner 3-Day Split",
  "description": "Simple 3-day program for beginners",
  "durationWeeks": 8,
  "isPublic": true
}
```

**Configure Template (Minimal):**
```json
{
  "workoutProgramId": "wp_example",
  "trainingDaysPerWeek": 3,
  "days": [
    {
      "dayNumber": 1,
      "name": "Day 1",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Regular",
          "restSeconds": 60,
          "exercises": [
            {
              "exerciseId": "e_pushups",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "8-12",
              "targetWeight": null,
              "targetRpe": "7"
            }
          ]
        }
      ]
    },
    {
      "dayNumber": 2,
      "name": "Day 2",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Regular",
          "restSeconds": 60,
          "exercises": [
            {
              "exerciseId": "e_squats",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "10-15",
              "targetWeight": null,
              "targetRpe": "7"
            }
          ]
        }
      ]
    },
    {
      "dayNumber": 3,
      "name": "Day 3",
      "setGroups": [
        {
          "orderIndex": 0,
          "type": "Regular",
          "restSeconds": 60,
          "exercises": [
            {
              "exerciseId": "e_pullups",
              "orderIndex": 0,
              "targetSets": 3,
              "targetReps": "5-8",
              "targetWeight": null,
              "targetRpe": "8"
            }
          ]
        }
      ]
    }
  ]
}
```

---

## Notes for Testing

1. **Exercise IDs**: The `exerciseId` values in these examples are placeholder UUIDs. You'll need to replace them with actual exercise IDs from your database.

2. **Program ID**: After creating a program in step 1, use the returned `programId` in all subsequent requests.

3. **Authentication**: All endpoints require authentication. Make sure to include appropriate authentication headers.

4. **Validation**: The API includes comprehensive validation, so make sure all required fields are provided and within acceptable ranges.

5. **Set Types**:
   - `"Regular"`: Single exercise with rest
   - `"Superset"`: 2 exercises back-to-back
   - `"GiantSet"`: 3+ exercises in sequence

6. **Rep Formats**: 
   - Range: `"6-8"`, `"10-15"`
   - Specific: `"10"`
   - Special: `"AMRAP"` (As Many Reps As Possible)

7. **Weights**: Can be `null` for bodyweight exercises or specific decimal values like `225.0`

8. **RPE**: Rate of Perceived Exertion, typically `"6-10"` or ranges like `"7-8"`