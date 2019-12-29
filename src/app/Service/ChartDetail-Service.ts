import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PostelCode } from '../Model/ChartDetail-Model';
import { Observable, throwError, from } from 'rxjs';
import { retry, catchError } from 'rxjs/operators'
import { environment } from '../../environments/environment';

@Injectable()

export class ChartDetailService {
    public baseUrl = 'https://api.postalpincode.in/pincode/';//environment.baseUrl;

    constructor(private http: HttpClient) { };

    getPostelCode(Pcode: number): Observable<PostelCode> {
        console.log(this.baseUrl);
        return this.http.get<PostelCode>(this.baseUrl + '612803').pipe(retry(1), catchError(this.errorHandl));
    }

    // Error handling
    errorHandl(error) {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
            // Get client-side error
            errorMessage = error.error.message;
        } else {
            // Get server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        console.log(errorMessage);
        return throwError(errorMessage);
    }

}
