echo off;
close all;
clear all;

s = 1.0 / 8.0*[-1 2 -1;
    2 4 2; 
    -1 2 -1];
f=imread('autumn.jpg');

% f1=double(f);
% n=rand(451,800)*.50;
% f1(:,:,1)=f1(:,:,1)+n;
% f1(:,:,2)=f1(:,:,2)+n;
% f1(:,:,3)=f1(:,:,3)+n;

f2=uint8(f);


m = fspecial('gaussian',2,2);
%figure;imshow(f);
figure;imshow(f2);
figure; imshow(imfilter(f2,m));

m_x = fspecial('gaussian',[1 50]);
m_y = fspecial('gaussian',[50 1]);
imgX = imfilter(f2,m_x);
imgXY = imfilter(imgX,m_y);
figure; imshow(imgXY);
