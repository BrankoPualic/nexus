import { Component, OnInit } from '@angular/core';
import { PageLoaderService } from '../../services/page-loader.service';

@Component({
  selector: 'app-spinner',
  standalone: true,
  imports: [],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss',
})
export class SpinnerComponent implements OnInit {
  isVisible = false;

  constructor(private pageLoader: PageLoaderService) {}

  ngOnInit(): void {
    this.pageLoader.loaderState$.subscribe({
      next: (state: boolean) => (this.isVisible = state),
    });
  }
}
