import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

export interface LoginRequest {
  API_Action: string;
  Device_Id: string;
  Sync_Time: string;
  Company_Code: string;
  API_Body: {
    Username: string;
    Pw: string;
  };
}

export interface UserLocation {
  Location_Code: string;
  Location_Name: string;
}

export interface LoginUserData {
  User_Code?: string;
  User_Display_Name?: string;
  Email?: string;
  User_Employee_Code?: string;
  Company_Code?: string;
  User_Locations?: UserLocation[];
}

export interface LoginResponse {
  Status_Code?: number;
  Sync_Time?: string;
  Message?: string;
  Response_Body?: LoginUserData[];

  // Normalized locations used by the Angular application.
  User_Locations?: UserLocation[];

  [key: string]: any;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl =
    'https://ez-staging-api.azurewebsites.net/api/External_Api/POS_Api/Invoke';

  private readonly sessionKey =
    'purchase_bill_auth';

  constructor(
    private http: HttpClient
  ) {}

  login(
    email: string,
    password: string
  ): Observable<LoginResponse> {

    const loginData: LoginRequest = {

      API_Action: 'GetLoginData',

      Device_Id: 'D001',

      Sync_Time: '',

      Company_Code: email,

      API_Body: {
        Username: email,
        Pw: password
      }

    };

    console.log(
      'Login request:',
      {
        ...loginData,
        API_Body: {
          Username: email,
          Pw: '********'
        }
      }
    );

    return this.http.post<LoginResponse>(
      this.apiUrl,
      loginData
    ).pipe(

      map(response => {

        console.log(
          'Login API raw response:',
          response
        );

        /*
         * The API returns the logged-in user inside:
         *
         * Response_Body[0]
         *
         * and User_Locations is inside that object.
         */

        const responseBody =
          response.Response_Body;

        if (
          !Array.isArray(responseBody) ||
          responseBody.length === 0
        ) {
          throw new Error(
            'Invalid login credentials or no user data was returned.'
          );
        }

        const userData =
          responseBody[0];

        const userLocations =
          userData.User_Locations ?? [];

        if (userLocations.length === 0) {
          throw new Error(
            'Login was successful, but no user locations were returned.'
          );
        }

        /*
         * Normalize the response so the rest of
         * the Angular application can use:
         *
         * response.User_Locations
         */

        const normalizedResponse: LoginResponse = {

          ...response,

          User_Locations:
            userLocations

        };

        console.log(
          'User locations:',
          userLocations
        );

        /*
         * Store the authenticated session.
         *
         * The password is NOT stored.
         */

        sessionStorage.setItem(
          this.sessionKey,
          JSON.stringify(
            normalizedResponse
          )
        );

        return normalizedResponse;
      })
    );
  }

  isAuthenticated(): boolean {

    return sessionStorage.getItem(
      this.sessionKey
    ) !== null;
  }

  getSession(): LoginResponse | null {

    const session =
      sessionStorage.getItem(
        this.sessionKey
      );

    if (!session) {
      return null;
    }

    try {

      return JSON.parse(
        session
      ) as LoginResponse;

    } catch {

      return null;
    }
  }

  logout(): void {

    sessionStorage.removeItem(
      this.sessionKey
    );
  }

  getUserLocations(): UserLocation[] {

    const session =
      this.getSession();

    return session?.User_Locations ?? [];
  }
}