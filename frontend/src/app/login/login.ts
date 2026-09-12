import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../services/auth';
import { LocationDetailService } from '../services/location-detail.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {

  email = '';
  password = '';

  errorMessage = '';
  isLoading = false;

  constructor(
    private authService: AuthService,
    private locationService: LocationDetailService,
    private router: Router
  ) {}

  login(): void {

    this.errorMessage = '';

    if (!this.email.trim()) {
      this.errorMessage = 'Email is required.';
      return;
    }

    if (!this.password) {
      this.errorMessage = 'Password is required.';
      return;
    }

    this.isLoading = true;

    this.authService.login(
      this.email.trim(),
      this.password
    ).subscribe({

      next: (response) => {

        console.log('Login API response:', response);

        const userLocations =
          response.User_Locations ?? [];

        if (userLocations.length === 0) {

          this.errorMessage =
            'Login was successful, but no user locations were returned.';

          this.isLoading = false;

          return;
        }

        const locationsToSave =
          userLocations.map(location => ({
            locationCode: location.Location_Code,
            locationName: location.Location_Name
          }));

        console.log(
          'Locations to save:',
          locationsToSave
        );

        this.locationService.sync(
          locationsToSave
        ).subscribe({

          next: (syncResponse) => {

            console.log(
              'Locations synchronized:',
              syncResponse
            );

            this.isLoading = false;

            this.router.navigate([
              '/purchase-bill'
            ]);

          },

          error: (error) => {

            console.error(
              'Location synchronization error:',
              error
            );

            this.isLoading = false;

            this.errorMessage =
              'Login succeeded, but user locations could not be saved. Please try again.';

          }

        });

      },

      error: (error) => {

        console.error(
          'Login error:',
          error
        );

        this.isLoading = false;

        if (error?.error?.Message) {

          this.errorMessage =
            error.error.Message;

        } else if (error?.error?.message) {

          this.errorMessage =
            error.error.message;

        } else {

          this.errorMessage =
            'Login failed. Please check your email and password.';

        }

      }

    });
  }
}