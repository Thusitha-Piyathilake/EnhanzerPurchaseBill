import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { LocationDetail } from '../models/location-detail';
import { LocationDetailService } from '../services/location-detail.service';

@Component({
  selector: 'app-location-details',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './location-details.html',
  styleUrl: './location-details.scss'
})
export class LocationDetailsComponent implements OnInit {

  locations: LocationDetail[] = [];

  newLocation: LocationDetail = {
    id: 0,
    locationCode: '',
    locationName: ''
  };

  editingId: number | null = null;

  isSaving = false;

  constructor(
    private locationService: LocationDetailService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

    console.log('LocationDetailsComponent loaded');

    // Load locations automatically when page opens
    this.loadLocations();
  }

  loadLocations(): void {

    console.log('Loading locations...');

    this.locationService.getAll().subscribe({

      next: (data) => {

        console.log('API RESPONSE:', data);
        console.log('Number of locations:', data.length);

        this.locations = data;

        console.log('locations variable:', this.locations);

        // Force Angular to update the HTML immediately
        this.cdr.detectChanges();
      },

      error: (error) => {

        console.error('GET LOCATIONS ERROR:', error);

        alert(
          'Failed to load locations. Check the browser console.'
        );
      }

    });
  }

  addLocation(): void {

    if (this.isSaving) {
      return;
    }

    const locationCode =
      this.newLocation.locationCode.trim();

    const locationName =
      this.newLocation.locationName.trim();

    if (!locationCode || !locationName) {

      alert(
        'Please enter Location Code and Location Name.'
      );

      return;
    }

    this.isSaving = true;

    this.locationService.create({
      locationCode: locationCode,
      locationName: locationName
    }).subscribe({

      next: (createdLocation) => {

        console.log(
          'Location created:',
          createdLocation
        );

        this.newLocation = {
          id: 0,
          locationCode: '',
          locationName: ''
        };

        this.isSaving = false;

        this.loadLocations();
      },

      error: (error) => {

        console.error(
          'Error creating location:',
          error
        );

        this.isSaving = false;

        alert(
          'Failed to create location.'
        );
      }

    });
  }

  editLocation(location: LocationDetail): void {

    this.editingId = location.id;

    this.newLocation = {
      id: location.id,
      locationCode: location.locationCode,
      locationName: location.locationName
    };

    // Update the form immediately
    this.cdr.detectChanges();
  }

  updateLocation(): void {

    if (
      this.editingId === null ||
      this.isSaving
    ) {
      return;
    }

    const locationCode =
      this.newLocation.locationCode.trim();

    const locationName =
      this.newLocation.locationName.trim();

    if (!locationCode || !locationName) {

      alert(
        'Please enter Location Code and Location Name.'
      );

      return;
    }

    this.isSaving = true;

    this.locationService.update(
      this.editingId,
      {
        locationCode: locationCode,
        locationName: locationName
      }
    ).subscribe({

      next: () => {

        console.log('Location updated');

        this.isSaving = false;

        this.cancelEdit();

        this.loadLocations();
      },

      error: (error) => {

        console.error(
          'Error updating location:',
          error
        );

        this.isSaving = false;

        alert(
          'Failed to update location.'
        );
      }

    });
  }

  deleteLocation(id: number): void {

    if (
      !confirm(
        'Are you sure you want to delete this location?'
      )
    ) {
      return;
    }

    this.locationService.delete(id).subscribe({

      next: () => {

        console.log(
          'Location deleted:',
          id
        );

        this.loadLocations();
      },

      error: (error) => {

        console.error(
          'Error deleting location:',
          error
        );

        alert(
          'Failed to delete location.'
        );
      }

    });
  }

  cancelEdit(): void {

    this.editingId = null;

    this.newLocation = {
      id: 0,
      locationCode: '',
      locationName: ''
    };

    // Update the form immediately
    this.cdr.detectChanges();
  }
}