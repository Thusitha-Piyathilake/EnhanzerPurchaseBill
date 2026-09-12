import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { LocationDetail } from '../models/location-detail';
import { PurchaseBillItem } from '../models/purchase-bill-item';

import { LocationDetailService } from '../services/location-detail.service';
import { PurchaseBillItemService } from '../services/purchase-bill-item.service';
import { AuthService } from '../services/auth';

@Component({
  selector: 'app-purchase-bill',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './purchase-bill.html',
  styleUrl: './purchase-bill.scss'
})
export class PurchaseBill implements OnInit {

  // Available items required by the assignment
  items = [
    'Mango',
    'Apple',
    'Banana',
    'Orange',
    'Grapes',
    'Kiwi',
    'Strawberry'
  ];

  // Locations loaded from Location_Details table
  locations: LocationDetail[] = [];

  // Items currently added to the purchase bill
  purchaseBillItems: PurchaseBillItem[] = [];

  // Form values
  selectedItem = '';
  selectedBatch = '';

  standardCost: number | null = null;
  standardPrice: number | null = null;
  quantity: number | null = null;
  discount: number | null = 0;

  // Calculated values
  totalCost = 0;
  totalSelling = 0;

  // Messages
  errorMessage = '';
  successMessage = '';

  // Separate loading states
  isLoadingLocations = false;
  isLoadingItems = false;
  isAdding = false;

  constructor(
    private locationService: LocationDetailService,
    private purchaseBillService: PurchaseBillItemService,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadLocations();
    this.loadPurchaseBillItems();
  }

  // Load batches/locations from SQL Server
  loadLocations(): void {
    this.isLoadingLocations = true;

    this.locationService.getAll().subscribe({
      next: (data) => {
        this.locations = data;

        this.isLoadingLocations = false;

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error loading locations:', error);

        this.errorMessage =
          'Unable to load batches. Please try again.';

        this.isLoadingLocations = false;

        this.cdr.detectChanges();
      }
    });
  }

  // Load existing purchase bill items
  loadPurchaseBillItems(): void {
    this.isLoadingItems = true;

    this.purchaseBillService.getAll().subscribe({
      next: (data) => {
        this.purchaseBillItems = data;

        this.isLoadingItems = false;

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error loading purchase bill items:', error);

        this.errorMessage =
          'Unable to load purchase bill items.';

        this.isLoadingItems = false;

        this.cdr.detectChanges();
      }
    });
  }

  // Calculate totals whenever form values change
  calculateTotals(): void {
    const cost = this.standardCost ?? 0;
    const price = this.standardPrice ?? 0;
    const qty = this.quantity ?? 0;
    const discountValue = this.discount ?? 0;

    const grossCost = cost * qty;

    this.totalCost =
      grossCost - (grossCost * discountValue / 100);

    this.totalSelling =
      price * qty;
  }

  // Add the current item to the purchase bill
  addItem(): void {
    this.errorMessage = '';
    this.successMessage = '';

    // Validation
    if (!this.selectedItem) {
      this.errorMessage = 'Please select an item.';
      return;
    }

    if (!this.selectedBatch) {
      this.errorMessage = 'Please select a batch.';
      return;
    }

    if (
      this.standardCost === null ||
      this.standardCost < 0
    ) {
      this.errorMessage =
        'Please enter a valid Standard Cost.';
      return;
    }

    if (
      this.standardPrice === null ||
      this.standardPrice < 0
    ) {
      this.errorMessage =
        'Please enter a valid Standard Price.';
      return;
    }

    if (
      this.quantity === null ||
      this.quantity <= 0
    ) {
      this.errorMessage =
        'Quantity must be greater than 0.';
      return;
    }

    if (
      this.discount === null ||
      this.discount < 0 ||
      this.discount > 100
    ) {
      this.errorMessage =
        'Discount must be between 0 and 100.';
      return;
    }

    // Make sure calculations are up to date
    this.calculateTotals();

    const newItem: PurchaseBillItem = {
      id: 0,
      item: this.selectedItem,
      batch: this.selectedBatch,
      standardCost: this.standardCost,
      standardPrice: this.standardPrice,
      quantity: this.quantity,
      discount: this.discount,
      totalCost: this.totalCost,
      totalSelling: this.totalSelling
    };

    this.isAdding = true;

    // Save item through ASP.NET Core API
    this.purchaseBillService.create(newItem).subscribe({
      next: (createdItem) => {
        this.purchaseBillItems.push(createdItem);

        this.successMessage =
          'Item added successfully.';

        this.clearForm();

        this.isAdding = false;

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error adding purchase bill item:', error);

        this.errorMessage =
          error?.error?.message ||
          'Unable to add purchase bill item.';

        this.isAdding = false;

        this.cdr.detectChanges();
      }
    });
  }

  // Clear the form after adding an item
  clearForm(): void {
    this.selectedItem = '';
    this.selectedBatch = '';

    this.standardCost = null;
    this.standardPrice = null;
    this.quantity = null;
    this.discount = 0;

    this.totalCost = 0;
    this.totalSelling = 0;
  }

  // Delete an item
  deleteItem(id: number): void {
    this.errorMessage = '';
    this.successMessage = '';

    this.purchaseBillService.delete(id).subscribe({
      next: () => {
        this.purchaseBillItems =
          this.purchaseBillItems.filter(item => item.id !== id);

        this.successMessage =
          'Item deleted successfully.';

        this.cdr.detectChanges();
      },
      error: (error) => {
        console.error('Error deleting item:', error);

        this.errorMessage =
          error?.error?.message ||
          'Unable to delete item.';

        this.cdr.detectChanges();
      }
    });
  }

  // Logout the authenticated user
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  // Total number of rows/items
  get totalItems(): number {
    return this.purchaseBillItems.length;
  }

  // Total quantity of all rows
  get totalQuantity(): number {
    return this.purchaseBillItems.reduce(
      (total, item) => total + item.quantity,
      0
    );
  }
}