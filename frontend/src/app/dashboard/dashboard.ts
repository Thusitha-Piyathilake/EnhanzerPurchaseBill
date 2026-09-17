import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import {
  Chart,
  DoughnutController,
  ArcElement,
  Tooltip,
  Legend
} from 'chart.js';

import {
  LatestPurchaseOrder,
  OldestPurchaseOrderItem,
  ItemQuantity
} from '../models/dashboard';

import { DashboardService } from '../services/dashboard.service';

Chart.register(
  DoughnutController,
  ArcElement,
  Tooltip,
  Legend
);

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent
  implements OnInit, AfterViewInit, OnDestroy {

  latestOrders: LatestPurchaseOrder[] = [];

  oldestItems: OldestPurchaseOrderItem[] = [];

  itemQuantities: ItemQuantity[] = [];

  isLoadingOrders = false;

  isLoadingItems = false;

  isLoadingChart = false;

  errorMessage = '';

  private chart: Chart<'doughnut'> | null = null;

  private chartReady = false;

  constructor(
    private dashboardService: DashboardService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {

    this.loadLatestOrders();

    this.loadOldestItems();

    this.loadItemQuantities();
  }

  ngAfterViewInit(): void {

    this.chartReady = true;

    this.createChart();
  }

  ngOnDestroy(): void {

    this.destroyChart();
  }

  // ==========================================
  // Navigate to Purchase Bill
  // ==========================================

  goToPurchaseBill(): void {

    this.router.navigate([
      '/purchase-bill'
    ]);
  }

  // ==========================================
  // Latest Orders
  // ==========================================

  loadLatestOrders(): void {

    this.isLoadingOrders = true;

    this.dashboardService
      .getLatestOrders()
      .subscribe({

        next: (data) => {

          this.latestOrders = data;

          this.isLoadingOrders = false;

          this.cdr.detectChanges();
        },

        error: (error) => {

          console.error(
            'Error loading latest orders:',
            error
          );

          this.errorMessage =
            'Unable to load latest purchase orders.';

          this.isLoadingOrders = false;

          this.cdr.detectChanges();
        }
      });
  }

  // ==========================================
  // Oldest Items
  // ==========================================

  loadOldestItems(): void {

    this.isLoadingItems = true;

    this.dashboardService
      .getOldestItems()
      .subscribe({

        next: (data) => {

          this.oldestItems = data;

          this.isLoadingItems = false;

          this.cdr.detectChanges();
        },

        error: (error) => {

          console.error(
            'Error loading oldest items:',
            error
          );

          this.errorMessage =
            'Unable to load oldest purchase order items.';

          this.isLoadingItems = false;

          this.cdr.detectChanges();
        }
      });
  }

  // ==========================================
  // Item Quantities
  // ==========================================

  loadItemQuantities(): void {

    this.isLoadingChart = true;

    this.dashboardService
      .getItemQuantities()
      .subscribe({

        next: (data) => {

          this.itemQuantities = data;

          this.isLoadingChart = false;

          /*
           * Update the Angular view first so the
           * canvas created by *ngIf exists before
           * Chart.js tries to use it.
           */
          this.cdr.detectChanges();

          this.createChart();
        },

        error: (error) => {

          console.error(
            'Error loading item quantities:',
            error
          );

          this.errorMessage =
            'Unable to load item quantity chart.';

          this.isLoadingChart = false;

          this.cdr.detectChanges();
        }
      });
  }

  // ==========================================
  // Create Donut Chart
  // ==========================================

  private createChart(): void {

    if (!this.chartReady) {
      return;
    }

    const canvas =
      document.getElementById(
        'itemQuantityChart'
      ) as HTMLCanvasElement | null;

    if (!canvas) {
      return;
    }

    if (this.itemQuantities.length === 0) {
      return;
    }

    this.destroyChart();

    const labels =
      this.itemQuantities.map(
        item => item.itemName
      );

    const quantities =
      this.itemQuantities.map(
        item => item.quantity
      );

    // ==========================================
    // Chart Colors
    // ==========================================

    const colors = [
      '#3B82F6',
      '#10B981',
      '#F59E0B',
      '#EF4444',
      '#8B5CF6',
      '#EC4899',
      '#06B6D4'
    ];

    this.chart = new Chart(
      canvas,
      {
        type: 'doughnut',

        data: {
          labels,

          datasets: [
            {
              data: quantities,

              backgroundColor: labels.map(
                (_, index) =>
                  colors[index % colors.length]
              ),

              borderColor: '#ffffff',

              borderWidth: 2
            }
          ]
        },

        options: {
          responsive: true,

          maintainAspectRatio: false,

          plugins: {
            legend: {
              position: 'bottom'
            },

            tooltip: {
              callbacks: {

                label: (context) => {

                  const value =
                    context.raw as number;

                  return `${context.label}: ${value}`;
                }
              }
            }
          }
        }
      }
    );
  }

  private destroyChart(): void {

    if (this.chart) {

      this.chart.destroy();

      this.chart = null;
    }
  }
}